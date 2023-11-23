using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static IEC60335Develop.Models.Connection;

namespace IEC60335Develop.Models {
    public class WTConnectModel {
        public string IPAddr { get; set; }
        public string GPIBAddr { get; set; }
        public string SerialNum { get; set; }
        public Connection Connection { get; set; }
    }

}
