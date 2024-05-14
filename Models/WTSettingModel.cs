using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace IEC60335Develop.Models {
    public class WTSettingModel : BindableBase {

        public string Element { get; set; }
        public string VoltageRange { get; set; }
        public string CurrentRange { get; set; }
    }
}
