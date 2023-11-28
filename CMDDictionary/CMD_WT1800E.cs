using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IEC60335Develop.CMDDictionary {
    public class CMD_WT1800E {
        const string Current_Range_Collectively = ":INPut:CURRent:RANGe:";
        const string Voltage_Range_Collectively = ":INPut:VOLTage:RANGe:";

        public class Queries {
            public const string IDN= "*IDN?";
            public string HighSpeed_Data(int NRF = 3) {
                return ":NUMeric:HSPeed:VALue? " + NRF;
            }

            public string HighSpeed_Max(int NRF = 3) {
                return ":NUMeric:HSPeed:MAXimum? " + NRF;
            }

            public string HighSpeed_Min(int NRF = 3) {
                return ":NUMeric:HSPeed:MINimum? " + NRF;
            }

            public string Current_Range(int element = 1) {
                return Current_Range_Collectively + "ELEMent" + element + "?";
            }
            public string Voltage_Range(int element = 1) {
                return Voltage_Range_Collectively + "ELEMent" + element + "?";
            }
            public string Current_Range() {
                return Current_Range_Collectively + "ALL" + "?";
            }
            public string Voltage_Range() {
                return Voltage_Range_Collectively + "ALL" + "?";
            }
        }
        public class Set {
            
            public const string HighSpeed_Start = ":HSPEED:START";
            public const string HighSpeed_Stop = ":HSPEED:STOP";

            public string Current_Range(string element = "ALL ", string current = "50A") {

                return Current_Range_Collectively + element + " " + current;
            }

            public string Voltage_Range(string element = "ALL", string voltage = "50A") {

                return Voltage_Range_Collectively + element + " " + voltage;
            }
        }


    }
}
