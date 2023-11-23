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

namespace IEC60335Develop.ViewModels {
    public class MeasureViewModel : BindableBase {
        private WTMeasureModel _wTMeasureModel;

        public WTMeasureModel WTMeasureModel {
            get { return _wTMeasureModel; }
            set { SetProperty(ref _wTMeasureModel, value); }
        }

        public DelegateCommand StartCommand { get; set; }

        public DelegateCommand StopCommand { get; set; }

        public DelegateCommand ResetCommand { get; set; }

        public DelegateCommand SaveFile { get; set; }

        public static string StartTimeCopyToReportViewModel { get; set; }

        public static string StopTimeCopyToReportViewModel { get; set; }

        public static List<double> CurrentListCopyToReportViewModel { get; set; }
        public static List<double> PowerListCopyToReportViewModel { get; set; }

        public Task TaskForGetValue { get; set; }
        public CancellationTokenSource cancellationToken { get; set; }

        string RelativePath;
        public static string SavePath { get; set; }
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


        public MeasureViewModel() {
            WTMeasureModel = new WTMeasureModel();

            StartCommand = new DelegateCommand(StartClick);
            StopCommand = new DelegateCommand(StopClick);
            ResetCommand = new DelegateCommand(ResetClick);
            SaveFile = new DelegateCommand(SaveFileClick);

            CurrentListCopyToReportViewModel = new List<double>();
            PowerListCopyToReportViewModel = new List<double>();

            Model = new PlotModel() { Title = "实时电流/功率曲线" };
            Series1 = new LineSeries { Title = "电流", MarkerType = MarkerType.None, Smooth = true };
            Series2 = new LineSeries { Title = "功率", MarkerType = MarkerType.None, Smooth = true, MarkerStroke = OxyColors.Red };

            dateTimeAxis1 = new DateTimeAxis() { MaximumRange = 0.1, StringFormat = "HH:mm", };
            dateTimeAxis1.Title = "Time";
            Model.Axes.Add(dateTimeAxis1);
            Model.Series.Add(Series1);
            Model.Series.Add(Series2);

            RelativePath = @"../../../File Folder";
        }

        private void SaveFileClick() {
            IsSaveFileButton = true;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = @"D:";
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
                SavePath = RelativePath + "/DataFile-00" + StopClickCount.ToString() + " " + date + " " + time + ".csv";
            }
            else {
                var date = DateTime.Now.ToString("yyyyMMdd");
                var time = DateTime.Now.ToString("HH.mm");
                SavePath = RelativePath + "/DataFile-00" + StopClickCount.ToString() + " " + date + " " + time + ".csv";
            }

            ConnectionViewModel.WT1800.RemoteCTRL(":HSPEED:STOP");//解注释
            StopTimeCopyToReportViewModel = DateTime.Now.ToString();
            //MessageBox.Show(StopTimeCopyToReportViewModel.ToString());//测试用
            cancellationToken.Cancel();

            CurrentListCopyToReportViewModel = WTMeasureModel.CurrentValue;
            PowerListCopyToReportViewModel = WTMeasureModel.PowerValue;


            if (!File.Exists(SavePath))
                File.Create(SavePath).Close();

            StreamWriter sw = new StreamWriter(SavePath, true, Encoding.UTF8);
            string dataHeader = "电压,电流,功率";
            sw.WriteLine(dataHeader);
            for (int j = 0; j < WTMeasureModel.VoltageValue.Count; j++) {
                sw.WriteLine($"{WTMeasureModel.VoltageValue[j]},{WTMeasureModel.CurrentValue[j]},{WTMeasureModel.PowerValue[j]}");
            }
            sw.Flush();
            sw.Close();
        }

        private void StartClick() {
            ConnectionViewModel.WT1800.RemoteCTRL(":HSPEED:START");//解注释
            StartTimeCopyToReportViewModel = DateTime.Now.ToString();
            WTMeasureModel.VoltageValue = new List<double>();
            WTMeasureModel.CurrentValue = new List<double>();
            WTMeasureModel.PowerValue = new List<double>();
            cancellationToken = new CancellationTokenSource();   //cancellationToken每次Cancel（StopClick中）需要重新new
            TaskForGetValue = Task.Run(GetValue, cancellationToken.Token);

        }

        private void GetValue() {
            while (true) {

                if (cancellationToken.Token.IsCancellationRequested) {
                    break;
                }

                //======= 测试用 ===================

                //Random rd = new Random();//测试用
                //var sv1 = rd.Next(10, 30);
                //var sv2 = rd.Next(10, 30);
                //Series1.Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, sv1));
                //Series2.Points.Add(DateTimeAxis.CreateDataPoint(DateTime.Now, sv2));
                //WTMeasureModel.VoltageValue.Add(sv1);
                //WTMeasureModel.CurrentValue.Add(sv1);
                //WTMeasureModel.PowerValue.Add(sv2);
                //App.Current.Dispatcher.BeginInvoke(new Action(
                //    () => {
                //        WTMeasureModel.VoltageValueRT = sv1.ToString();
                //        WTMeasureModel.CurrentValueRT = sv1.ToString();
                //        WTMeasureModel.PowerValueRT = sv2.ToString();
                //        WTMeasureModel.PowerMaxValue = sv2.ToString();
                //    }));
                //if (Series1.Points.Count > 300) {
                //    Series1.Points.RemoveAt(0);
                //    Series2.Points.RemoveAt(0);
                //}
                //Model.InvalidatePlot(true);
                //DelayOperation.DelaySomeTime(20);
                //================================


                string voltageValue = string.Empty;
                while (true) {
                    voltageValue = ConnectionViewModel.WT1800.RemoteCTRL(":NUMeric:HSPeed:VALue? " + (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) + 2 * (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) - 1)).ToString());
                    if (!string.IsNullOrWhiteSpace(voltageValue)) {
                        break;
                    }
                }

                var currentValue = ConnectionViewModel.WT1800.RemoteCTRL(":NUMeric:HSPeed:VALue? " + (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) + 1 + 2 * (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) - 1)).ToString());
                var powerValue = ConnectionViewModel.WT1800.RemoteCTRL(":NUMeric:HSPeed:VALue? " + (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) + 2 + 2 * (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) - 1)).ToString());
                var powerMaxValue = ConnectionViewModel.WT1800.RemoteCTRL(":NUMERIC:HSPEED:MAXIMUM? " + (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) + 2 + 2 * (double.Parse(SettingViewModel.ElementCopyToMesViewModel.Substring(7)) - 1)).ToString());


                var voltageValueArray = Array.ConvertAll<string, double>(voltageValue.Split(','), s => double.Parse(s));
                var currentValueArray = Array.ConvertAll<string, double>(currentValue.Split(','), s => double.Parse(s));
                var powerValueArray = Array.ConvertAll<string, double>(powerValue.Split(','), s => double.Parse(s));

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

