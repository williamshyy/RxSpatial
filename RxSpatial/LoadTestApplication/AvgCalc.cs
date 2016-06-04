using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestApplication
{
    class AvgCalc
    {
        int cnter = 0;
        double avg = 0;
        public double AvgLatency(double latency)
        {
            avg = avg +(latency-avg) / (cnter + 1);
            cnter++;
            return avg;
        }
        public double Avg { get { return avg; } }
    }
}
