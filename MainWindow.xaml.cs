using IEC60335Develop.ViewModels;
using IEC60335Develop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IEC60335Develop {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = tabControl.SelectedItem as TabItem;
            if (item.Header.ToString() == "测量") {
                if (SettingViewModel.IsClicked == true) {
                    return;
                }
                else {
                    MessageBox.Show("请先在设定页面发送设定", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                }
                //var settingUserControl = Application.Current.Windows.Cast<UserControl>().FirstOrDefault(userControl => userControl is SettingView) as SettingView;
            }
        }
    }
}
