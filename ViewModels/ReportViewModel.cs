using IEC60335Develop.Models;
using OxyPlot;
//using OxyPlot.Wpf;
using LineSeries = OxyPlot.Series.LineSeries;
using OxyPlot.Axes;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;
using System.IO;

namespace IEC60335Develop.ViewModels {
    public class ReportViewModel : BindableBase {

        private WTReportModel _wTReportModel;
        public WTReportModel WTReportModel {
            get { return _wTReportModel; }
            set { SetProperty(ref _wTReportModel, value); }
        }

        private PlotModel __modelCurrentSortl;
        public PlotModel ModelCurrentSort {
            get { return __modelCurrentSortl; }
            set { SetProperty(ref __modelCurrentSortl, value); }
        }

        public LineSeries Series1 { get; set; }
        public DateTimeAxis dateTimeAxis1 { get; set; }


        private PlotModel _modelPowerSort;
        public PlotModel ModelPowerSort {
            get { return _modelPowerSort; }
            set { SetProperty(ref _modelPowerSort, value); }
        }

        public LineSeries Series2 { get; set; }
        public DateTimeAxis dateTimeAxis2 { get; set; }

        private Collection<Elements> _elementsCollection;
        public Collection<Elements> ElementsCollection {
            get { return _elementsCollection; }
            set { SetProperty(ref _elementsCollection, value); }
        }

        private int _defaultTimeInterval;
        public int DefaultTimeInterval {
            get { return _defaultTimeInterval; }
            set { SetProperty(ref _defaultTimeInterval, value); }
        }


        public string SavePathSorted { get; set; }

        public DelegateCommand SortOutputCommand { get; set; }

        public ReportViewModel() {


            WTReportModel = new WTReportModel();
            WTReportModel.CurrentList = new List<double>();
            WTReportModel.PowerList = new List<double>();

            SortOutputCommand = new DelegateCommand(SortOutputClick);

            ModelCurrentSort = new PlotModel() { Title = "排序电流曲线" };
            Series1 = new LineSeries { Title = "电流", MarkerType = MarkerType.None, Smooth = true };
            dateTimeAxis1 = new DateTimeAxis() { MaximumRange = 0.1, };
            dateTimeAxis1.Title = "Time";
            ModelCurrentSort.Axes.Add(dateTimeAxis1);
            ModelCurrentSort.Series.Add(Series1);

            ModelPowerSort = new PlotModel() { Title = "排序功率曲线" };
            Series2 = new LineSeries { Title = "功率", MarkerType = MarkerType.None, Smooth = true };
            dateTimeAxis2 = new DateTimeAxis() { MaximumRange = 0.1, };
            dateTimeAxis2.Title = "Time";
            ModelPowerSort.Axes.Add(dateTimeAxis2);
            ModelPowerSort.Series.Add(Series2);


        }



        private void SortOutputClick() {

            //遍历，清除所有之前ModelCurrentSort绘制的曲线
            foreach (var lineSer in ModelCurrentSort.Series) {
                ((LineSeries)lineSer).Points.Clear();

            }
            //清除完曲线之后，重新刷新坐标轴
            dateTimeAxis1 = new DateTimeAxis() { MaximumRange = 0.1, };
            dateTimeAxis1.Title = "Time";
            ModelCurrentSort.InvalidatePlot(true);
            Thread.Sleep(10);

            //遍历，清除所有之前ModelPowerSort绘制的曲线
            foreach (var lineSer in ModelPowerSort.Series) {
                ((LineSeries)lineSer).Points.Clear();

            }
            //清除完曲线之后，重新刷新坐标轴
            dateTimeAxis2 = new DateTimeAxis() { MaximumRange = 0.1, };
            dateTimeAxis2.Title = "Time";
            ModelPowerSort.InvalidatePlot(true);
            Thread.Sleep(10);

            //将起止时间同步更新到View
            WTReportModel.StartTime = App.StartTimeCopyToReportViewModel;
            WTReportModel.StopTime = App.StopTimeCopyToReportViewModel;

            //将CurrentList、PowerList降序排列
            WTReportModel.CurrentList = App.CurrentListCopyToReportViewModel;
            WTReportModel.PowerList = App.PowerListCopyToReportViewModel;
            WTReportModel.CurrentList.Sort();
            WTReportModel.CurrentList.Reverse();
            WTReportModel.PowerList.Sort();
            WTReportModel.PowerList.Reverse();
            //计算最大、90%、平均值更新到View
            WTReportModel.MaxValue = WTReportModel.PowerList[0].ToString();
            WTReportModel.AvgValue = (WTReportModel.PowerList.Sum() / WTReportModel.PowerList.Count).ToString("0.00");
            int nintypoistion = (int)(WTReportModel.PowerList.Count * 0.1);
            WTReportModel.NintyValue = WTReportModel.PowerList[nintypoistion].ToString();
            var maxParse = double.TryParse(WTReportModel.MaxValue, out double max);
            var avgParse = double.TryParse(WTReportModel.AvgValue, out double avg);
            var nintyParse = double.TryParse(WTReportModel.NintyValue, out double ninty);
            //计算测量结果更新到View
            WTReportModel.Result = ((max > 2 * avg) ? (ninty > avg ? ninty : avg) : avg).ToString("0.00");

            //将结果写入DataGrid，将降序排列的List更新到View
            ElementsCollection = new Collection<Elements>();
            //DefaultTimeInterval = 20;
            int testLineNum = 0;
            DefaultTimeInterval = 1000 / App.DefaultTimeSpan;
            DateTime DateTimeStamp=Convert.ToDateTime(WTReportModel.StartTime);
            TimeSpan SpanUnit = TimeSpan.FromMilliseconds(DefaultTimeInterval);
            for (int i = 0; i < WTReportModel.PowerList.Count; i++) {
                Series1.Points.Add(new DataPoint(i, WTReportModel.CurrentList[i]));
                Series2.Points.Add(new DataPoint(i, WTReportModel.PowerList[i]));
                Elements elements = new Elements() {
                    Timestamp = DateTimeStamp.ToString("HH:mm:ss:fff"),
                    Current = WTReportModel.CurrentList[i],
                    Power = WTReportModel.PowerList[i]
                };
                ElementsCollection.Add(elements);
                DateTimeStamp += SpanUnit;
                testLineNum++;
            }
            LineSeries Series1Copy = new LineSeries();
            var tempPoints = Series1.Points.ToArray();
            Series1.Points.Clear();
            for (int i = 0; i < tempPoints.Length; i++) {
                Series1Copy.Points.Add(tempPoints[i]);
            }
            LineSeries Series2Copy = new LineSeries();
            var tempPoints2 = Series2.Points.ToArray();
            Series2.Points.Clear();
            for (int i = 0; i < tempPoints2.Length; i++) {
                Series2Copy.Points.Add(tempPoints2[i]);
            }
            ModelCurrentSort.Series.Add(Series1Copy);
            ModelPowerSort.Series.Add(Series2Copy);

            ModelCurrentSort.InvalidatePlot(true);
            ModelPowerSort.InvalidatePlot(true);

            //排序结果输出csv
            SavePathSorted = App.SavePath.Substring(0, App.SavePath.Length - 4) + "Sorted.csv";
            if (!File.Exists(SavePathSorted))
                File.Create(SavePathSorted).Close();

            StreamWriter sw = new StreamWriter(SavePathSorted, true, Encoding.UTF8);
            string dataHeader = "电流,功率";
            sw.WriteLine(dataHeader);
            for (int j = 0; j < WTReportModel.PowerList.Count; j++) {
                sw.WriteLine($"{WTReportModel.CurrentList[j]},{WTReportModel.PowerList[j]}");
            }
            sw.Flush();
            sw.Close();
        }


    }
    public class Elements {
        public string Timestamp { get; set; }
        public double Power { get; set; }
        public double Current { get; set; }
    }
}
