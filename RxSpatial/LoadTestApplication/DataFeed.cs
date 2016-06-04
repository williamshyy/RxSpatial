using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Microsoft.SqlServer.Types;
namespace LoadTestApplication
{
    class DataFeed
    {
        private BatchLoadParams.TestCase tc;
        Simulation s;
        int idx=0;
        public DataFeed(BatchLoadParams.TestCase tc,Simulation s)
        {
            //Console.WriteLine("DataFeed");
            this.s = s;
            this.tc = tc;
        }


        public static System.Timers.Timer timer=new System.Timers.Timer();
        public void scheduleDataFeed()
        {
            Console.WriteLine("Timer start ");
            //  DispatcherTimer setup
            timer.Elapsed += this.updateTraffic;
            timer.Interval = tc.updateTimeInterval;
            timer.Enabled = true;
        }
        private void updateTraffic(object sender, EventArgs e)
        {
            //this.scheduleDataFeed();
            Console.WriteLine("Timer tick ");
            if (tc.output==false||idx>=40)
            {
                timer.Enabled = false;
                timer.Elapsed -= this.updateTraffic;
               // tc.latency = tc.latencyRecorder.Avg;
                return;
            }
            s.callProcTraffic();
            idx++;
        }
     

    }
}
