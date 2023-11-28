using OxyPlot;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60335Develop.Models {
    public class WTMeasureModel:BindableBase {

        public List<double> VoltageValue { get; set; }
        public List<double> CurrentValue { get; set; }
        public List<double> PowerValue { get; set; }

        private string _powerMaxValue;
        public string PowerMaxValue {
            get { return _powerMaxValue; }
            set { SetProperty(ref _powerMaxValue, value); }
        }

        private string _voltageValueRT;
        public string VoltageValueRT {
            get { return _voltageValueRT; }
            set { SetProperty(ref _voltageValueRT, value); }
        }

        private string _currentValueRT;
        public string CurrentValueRT {
            get { return _currentValueRT; }
            set { SetProperty(ref _currentValueRT, value); }
        }

        private string _powerValueRT;
        public string PowerValueRT {
            get { return _powerValueRT; }
            set { SetProperty(ref _powerValueRT, value); }
        }


    }
}
