using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEC60335Develop.Models;
using Prism.Commands;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace IEC60335Develop.ViewModels {

    public enum ConnectWayEnum {
        Ether,
        GPIB,
        USBTMC
    }


    public class ConnectionViewModel : BindableBase {

        public ConnectionViewModel() {
            LoadWT();
            ConnectClickCommand = new DelegateCommand(ConnectWT);
            DisconnectClickCommand = new DelegateCommand(DisconnectWT);
        }

        public EnumToBooleanConverter EnumToBooleanConverter { get; set; }


        private ConnectWayEnum _connectWayRadioBtn;

        public ConnectWayEnum ConnectWayRadioBtn {
            get { return _connectWayRadioBtn; }
            set { SetProperty(ref _connectWayRadioBtn, value); }
        }

        private string _imageSource;

        public string ImageSource {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }


        private WTConnectModel _wTConnectModel;

        public WTConnectModel WTConnectModel {
            get { return _wTConnectModel; }
            set { SetProperty(ref _wTConnectModel, value); }
        }

        private string _idnInfo;

        public string IDNInfo {
            get { return _idnInfo; }
            set { SetProperty(ref _idnInfo, value); }
        }

       


        public DelegateCommand ConnectClickCommand { get; set; }

        public DelegateCommand DisconnectClickCommand { get; set; }

        public void ConnectWT() {
            if (ConnectWayRadioBtn.ToString() == "Ether") {
                App.WT1800 = new Connection((int)Connection.wire.VXI11, WTConnectModel.IPAddr);
                App.WT1800.Connect();
                if (App.WT1800.IsConnected == true) {
                    ImageSource = "pack://application:,,,/IEC60335Develop;component/Resources/Connect.png";
                    IDNInfo = App.WT1800.RemoteCTRL("*IDN?");
                }
                else {
                    MessageBox.Show("无法连接！请确认连接设置。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }
            else if (ConnectWayRadioBtn.ToString() == "USBTMC") {
                App.WT1800 = new Connection((int)Connection.wire.USBTMC, WTConnectModel.SerialNum);
                App.WT1800.Connect();
                if (App.WT1800.IsConnected == true) {
                    ImageSource = "pack://application:,,,/IEC60335Develop;component/Resources/Connect.png";
                    IDNInfo = App.WT1800.RemoteCTRL("*IDN?");
                }
                else {
                    MessageBox.Show("无法连接！请确认连接设置。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }
            else {
                App.WT1800 = new Connection((int)Connection.wire.GPIB, WTConnectModel.GPIBAddr);
                App.WT1800.Connect();
                if (App.WT1800.IsConnected == true) {
                    ImageSource = "pack://application:,,,/IEC60335Develop;component/Resources/Connect.png";
                    IDNInfo = App.WT1800.RemoteCTRL("*IDN?");
                }
                else {
                    MessageBox.Show("无法连接！请确认连接设置。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
            }

        }

        public void DisconnectWT() {
            App.WT1800.Finish();
            ImageSource = "pack://application:,,,/IEC60335Develop;component/Resources/Disconnect.png";
        }

        public void LoadWT() {
            this.WTConnectModel = new WTConnectModel();
            WTConnectModel.IPAddr = "192.168.1.10";
            WTConnectModel.GPIBAddr = "1";
            WTConnectModel.SerialNum = "C3TB03016E";
            ImageSource = "pack://application:,,,/IEC60335Develop;component/Resources/Disconnect.png";
        }


    }

    public class EnumToBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value == null ? false : value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
