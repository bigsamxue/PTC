using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60335Develop.ViewModels {
    internal class MainWindowViewModel :BindableBase {

		private bool _setHSPCompleted;
		public bool SetHSPCompleted {
			get { return _setHSPCompleted; }
			set { SetProperty(ref _setHSPCompleted, value); }
		}

        public MainWindowViewModel()
        {
            
        }
    }
}
