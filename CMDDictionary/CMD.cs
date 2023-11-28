﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IEC60335Develop.CMDDictionary {
    public class CMD {
        const string Current_Range_Collectively = ":INPut:CURRent:RANGe:";
        const string Voltage_Range_Collectively = ":INPut:VOLTage:RANGe:";

        public class Queries {
            public const string IDN= "*IDN?";
            public static string HighSpeed_Data(int NRF = 3) {
                return ":NUMeric:HSPeed:VALue? " + NRF;
            }

            public static string HighSpeed_Max(int NRF = 3) {
                return ":NUMeric:HSPeed:MAXimum? " + NRF;
            }

            public static string HighSpeed_Min(int NRF = 3) {
                return ":NUMeric:HSPeed:MINimum? " + NRF;
            }

            public static string Current_Range(int element = 1) {
                return Current_Range_Collectively + "ELEMent" + element + "?";
            }
            public static string Voltage_Range(int element = 1) {
                return Voltage_Range_Collectively + "ELEMent" + element + "?";
            }
            public static string Current_Range() {
                return Current_Range_Collectively + "ALL" + "?";
            }
            public static string Voltage_Range() {
                return Voltage_Range_Collectively + "ALL" + "?";
            }
        }
        public class Set {
            
            public const string HighSpeed_Start = ":HSPEED:START";
            public const string HighSpeed_Stop = ":HSPEED:STOP";

            public static string Current_Range(string element = "ALL ", string range = "50A") {

                return Current_Range_Collectively + element + " " + range;
            }

            public static string Voltage_Range(string element = "ALL", string range = "50A") {

                return Voltage_Range_Collectively + element + " " + range;
            }
        }


    }
}