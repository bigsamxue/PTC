using IEC60335Develop.Models;
using Microsoft.Win32;
using OxyPlot;
//using OxyPlot.Wpf;
using LineSeries = OxyPlot.Series.LineSeries;
using OxyPlot.Axes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IEC60335Develop.CMDDictionary;
using System.Diagnostics;

namespace IEC60335Develop.ViewModels {
    public class MeasureViewModel : BindableBase {
        private WTMeasureModel _wTMeasureModel;

        public WTMeasureModel WTMeasureModel {
            get { return _wTMeasureModel; }
            set { SetProperty(ref _wTMeasureModel, value); }
        }

        private int _delaySec;
        public int DelaySec {
            get { return _delaySec; }
            set { SetProperty(ref _delaySec, value); }
        }

        private bool _isNotMeasuring;
        public bool IsNotMeasuring {
            get { return _isNotMeasuring; }
            set { SetProperty(ref _isNotMeasuring, value); }
        }


        public DelegateCommand StartCommand { get; set; }

        public DelegateCommand StopCommand { get; set; }

        public DelegateCommand ResetCommand { get; set; }

        public DelegateCommand SaveFile { get; set; }



        public Task TaskForGetValue { get; set; }
        public CancellationTokenSource cancellationToken { get; set; }
        Stopwatch sw;

        string RelativePath;

        int StopClickCount = 0;
        bool IsSaveFileButton = false;


        private PlotModel _model;

        public PlotModel Model {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }
        public LineSeries Series1 { get; set; }
        public LineSeries Series2 { get; set; }
        public DateTimeAxis dateTimeAxis1 { get; set; }

        int elementNum;
        int volIndex;
        int curIndex;
        int powIndex;

        public MeasureViewModel() {
            WTMeasureModel = new WTMeasureModel();

            StartCommand = new DelegateCommand(StartClick);
            StopCommand = new DelegateCommand(StopClick);
            ResetCommand = new DelegateCommand(ResetClick);
            SaveFile = new DelegateCommand(SaveFileClick);

            App.CurrentListCopyToReportViewModel = new List<double>();
            App.PowerListCopyToReportViewModel = new List<double>();

            Model = new PlotModel() { Title = "实时电流/功率曲线" };
            Series1 = new LineSeries { Title = "电流", MarkerType = MarkerType.None, Smooth = true };
            Series2 = new LineSeries { Title = "功率", MarkerType = MarkerType.None, Smooth = true, MarkerStroke = OxyColors.Red };

            dateTimeAxis1 = new DateTimeAxis() { MaximumRange = 0.1, StringFormat = "HH:mm", };
            dateTimeAxis1.Title = "Time";
            Model.Axes.Add(dateTimeAxis1);
            Model.Series.Add(Series1);
            Model.Series.Add(Series2);

            RelativePath = App.DefaultFolderPath + @"/File Folder";
            DelaySec = 0;
            IsNotMeasuring = true;
            sw = new Stopwatch();
        }

        private void SaveFileClick() {
            IsSaveFileButton = true;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = App.DefaultFolderPath;
            dialog.ShowDialog();
            RelativePath = dialog.SelectedPath;

            //MessageBox.Show(RelativePath);//测试用

        }

        public void ResetClick() {
            //遍历，清除所有之前绘制的曲线
            foreach (var lineSer in Model.Series) {
                ((LineSeries)lineSer).Points.Clear();

            }
            //清除完曲线之后，重新刷新坐标轴
            dateTimeAxis1 = new DateTimeAxis() { MaximumRange = 0.1, StringFormat = "HH:mm", };
            dateTimeAxis1.Title = "Time";
            Model.InvalidatePlot(true);
            Thread.Sleep(10);
        }

        private void StopClick() {
            StopClickCount++;

            if (IsSaveFileButton == false) {
                if (!Directory.Exists(RelativePath)) {
                    Directory.CreateDirectory(RelativePath);
                }
                var date = DateTime.Now.ToString("yyyyMMdd");
                var time = DateTime.Now.ToString("HH.mm");
                App.SavePath = RelativePath + "/DataFile-00" + StopClickCount.ToString() + " " + date + " " + time + ".csv";
            }
            else {
                var date = DateTime.Now.ToString("yyyyMMdd");
                var time = DateTime.Now.ToString("HH.mm");
                App.SavePath = RelativePath + "/DataFile-00" + StopClickCount.ToString() + " " + date + " " + time + ".csv";
            }

            App.WT1800.RemoteCTRL(CMD.Set.HighSpeed_Stop);//解注释
            App.StopTimeCopyToReportViewModel = DateTime.Now.ToString();
            Interrupter();
            IsNotMeasuring = true;
            App.CurrentListCopyToReportViewModel = WTMeasureModel.CurrentValue;
            App.PowerListCopyToReportViewModel = WTMeasureModel.PowerValue;


            if (!File.Exists(App.SavePath))
                File.Create(App.SavePath).Close();

            using (StreamWriter sw = new StreamWriter(App.SavePath, true, Encoding.UTF8)) {
                string dataHeader = "电压,电流,功率";
                sw.WriteLine(dataHeader);
                for (int j = 0; j < WTMeasureModel.VoltageValue.Count; j++) {
                    sw.WriteLine($"{WTMeasureModel.VoltageValue[j]},{WTMeasureModel.CurrentValue[j]},{WTMeasureModel.PowerValue[j]}");
                }
                sw.Flush();
                sw.Close();
            }
        }

        void SetReceiverItemIndex() {
            App.WT1800.RemoteCTRL(CMD.Set.HSpeed_Item("1","U", elementNum.ToString()));
            App.WT1800.RemoteCTRL(CMD.Set.HSpeed_Item("2","I", elementNum.ToString()));
            App.WT1800.RemoteCTRL(CMD.Set.HSpeed_Item("3","P", elementNum.ToString()));
            throw new NotImplementedException();
        }

        private void StartClick() {

            App.WT1800.RemoteCTRL(CMD.Set.HighSpeed_Start);//解注释
            IsNotMeasuring = false;
            App.StartTimeCopyToReportViewModel = DateTime.Now.ToString();
            WTMeasureModel.VoltageValue = new List<double>();
            WTMeasureModel.CurrentValue = new List<double>();
            WTMeasureModel.PowerValue = new List<double>();

            cancellationToken = new CancellationTokenSource();   //cancellationToken每次Cancel（StopClick中）需要重新new
            elementNum = Int32.Parse(App.ElementCopyToMesViewModel.Substring(7));
            volIndex = 1;
            curIndex = 2;
            powIndex = 3;
            if (DelaySec != 0) {
                sw.Reset();
                Task.Run(()=>Delayer(DelaySec));
            }

            TaskForGetValue = Task.Run(GetValue, cancellationToken.Token);

        }
        void Delayer(int delay) {
            long timespan=delay*1000;
            sw.Start();
            while(sw.ElapsedMilliseconds < timespan) { }
            StopClick();
            sw.Stop();
        }
        void Interrupter() {
            cancellationToken.Cancel();
            IsNotMeasuring=true;
        }
        double[] ValueConvert(string oriData) {
            if (oriData.Contains("Error")|string.IsNullOrWhiteSpace(oriData)) return null;
            return Array.ConvertAll<string, double>(oriData.Split(','), double.Parse);
        }

        string GetHighSpeedData(int index) {
            return App.WT1800.RemoteCTRL(CMD.Queries.HighSpeed_Data(index)).Replace("\n","");
        }


        private void GetValue() {
            while (true) {
                if (cancellationToken.Token.IsCancellationRequested) {
                    break;
                }

                string voltageValue = string.Empty;
                while (true) {
                    voltageValue = GetHighSpeedData(volIndex);
                    if (!string.IsNullOrWhiteSpace(voltageValue)) {
                        break;
                    }
                }
                var currentValue = GetHighSpeedData(curIndex);
                var powerValue = GetHighSpeedData(powIndex);
                var powerMaxValue = App.WT1800.RemoteCTRL(CMD.Queries.HighSpeed_Max(powIndex));

                var voltageValueArray = ValueConvert(voltageValue);
                var currentValueArray = ValueConvert(currentValue);
                var powerValueArray = ValueConvert(powerValue);

                if (voltageValueArray == null) {
                    MessageBox.Show("未获取到有效数据，请检查电源");
                    return;
                }
                for (int i = 0; i < currentValueArray.Length; i++) {
                    var date = DateTime.Now;
                    Series1.Points.Add(DateTimeAxis.CreateDataPoint(date, currentValueArray[i]));
                    Series2.Points.Add(DateTimeAxis.CreateDataPoint(date, powerValueArray[i]));
                    WTMeasureModel.VoltageValue.Add(voltageValueArray[i]);
                    WTMeasureModel.CurrentValue.Add(currentValueArray[i]);
                    WTMeasureModel.PowerValue.Add(powerValueArray[i]);

                    DelayOperation.DelaySomeTime(20);
                }

                App.Current.Dispatcher.BeginInvoke(new Action(() => {
                    WTMeasureModel.VoltageValueRT = voltageValueArray[voltageValueArray.Length - 1].ToString();
                    WTMeasureModel.CurrentValueRT = currentValueArray[currentValueArray.Length - 1].ToString();
                    WTMeasureModel.PowerValueRT = powerValueArray[powerValueArray.Length - 1].ToString();
                    WTMeasureModel.PowerMaxValue = double.Parse(powerMaxValue).ToString();
                }));

                if (Series1.Points.Count > 300) {
                    Series1.Points.RemoveAt(0);
                    Series2.Points.RemoveAt(0);
                }
                Model.InvalidatePlot(true);
            }
        }

    }
}

