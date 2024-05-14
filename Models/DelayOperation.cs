using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEC60335Develop.Models {
    public  class DelayOperation {
        public static void DelaySomeTime(double time) {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (stopwatch.Elapsed.TotalMilliseconds < time) { }
            stopwatch.Stop();
        }
    }
}
