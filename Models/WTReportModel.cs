using IEC60335Develop.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60335Develop.Models {
    public class WTReportModel:BindableBase {
		private string _startTime;
		public string StartTime {
			get { return _startTime; }
			set { SetProperty(ref _startTime, value); }
		}

		private string _stopTime;
		public string StopTime {
			get { return _stopTime; }
			set { SetProperty(ref _stopTime, value); }
		}

		private string _result;
		public string Result {
			get { return _result; }
			set { SetProperty(ref _result, value); }
		}

		private string _maxValue;
		public string MaxValue {
			get { return _maxValue; }
			set { SetProperty(ref _maxValue, value); }
		}

		private string _avgValue;
		public string AvgValue {
			get { return _avgValue; }
			set { SetProperty(ref _avgValue, value); }
		}

		private string _nintyValue;
		public string NintyValue {
			get { return _nintyValue; }
			set { SetProperty(ref _nintyValue, value); }
		}

		public List<double> CurrentList { get; set; }
		public List<double> PowerList { get; set; }
	}
}
