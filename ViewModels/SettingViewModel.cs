using IEC60335Develop.CMDDictionary;
using IEC60335Develop.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IEC60335Develop.ViewModels {
    public class SettingViewModel : BindableBase {

        private List<string> _elementItems;

        public List<string> ElementItems {
            get { return _elementItems; }
            set { SetProperty(ref _elementItems, value); }
        }

        private List<string> _voltageRangeItems;

        public List<string> VoltageRangeItems {
            get { return _voltageRangeItems; }
            set { SetProperty(ref _voltageRangeItems, value); }
        }

        private List<string> _currentRangeItems;

        public List<string> CurrentRangeItems {
            get { return _currentRangeItems; }
            set { SetProperty(ref _currentRangeItems, value); }
        }


        private WTSettingModel _wTSettingModel;

        public WTSettingModel WTSettingModel {
            get { return _wTSettingModel; }
            set { SetProperty(ref _wTSettingModel, value); }
        }

        public static bool IsClicked { get; set; } = false;

        public DelegateCommand SendSettingClickCommand { get; set; }

        public SettingViewModel() {
            WTSettingModel = new WTSettingModel();
            ElementItems = new List<string> { "Element1", "Element2", "Element3", "Element4", "Element5", "Element6" };
            VoltageRangeItems = new List<string> { "1.5V", "3V", "6V", "10V", "15V", "30V", "60V", "100V", "150V", "300V", "600V", "1000V" };
            CurrentRangeItems = new List<string> { "10mA", "20mA", "50mA", "100mA", "200mA", "500mA", "1A", "2A", "5A", "10A", "20A", "50A" };
            SendSettingClickCommand = new DelegateCommand(SendSettingToWT);
        }
        public void SendSettingToWT() {
            IsClicked = true;
            App.WT1800.RemoteCTRL(CMD.Set.Current_Range(element: WTSettingModel.Element, range: WTSettingModel.CurrentRange));
            App.WT1800.RemoteCTRL(CMD.Set.Voltage_Range(element: WTSettingModel.Element, range: WTSettingModel.VoltageRange));
            App.WT1800.RemoteCTRL(CMD.Set.HighSpeed_Mode);
            App.ElementCopyToMesViewModel = WTSettingModel.Element;

        }
    }

}

