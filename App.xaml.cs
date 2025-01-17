﻿using IEC60335Develop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IEC60335Develop {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public static Connection WT1800 { get; set; }

        public static string ElementCopyToMesViewModel { get; set; }

        public static string StartTimeCopyToReportViewModel { get; set; }

        public static string StopTimeCopyToReportViewModel { get; set; }

        public static List<double> CurrentListCopyToReportViewModel { get; set; }

        public static List<double> PowerListCopyToReportViewModel { get; set; }

        public static string SavePath { get; set; }
    }
}
