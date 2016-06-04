using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
namespace LoadTestApplication
{

    enum Operation { Add, Modify, Delete, NA };
    class ObjAction
    {
        private int _id; private Operation _operation; private SqlGeography _loc;
        DateTime startTime;
        DateTime notifyCompleteTime;
        TimeSpan timeCostToFinishProc;
        public Double latency = -1;
        int notifiedCnt = 0;
        private int obsvrCnt;
        public void timerStart() { startTime = HighResolutionDateTime.UtcNow; }
        public void updateObsvrCnt(int cnt) { obsvrCnt = cnt; }
        public void updateTimeInterval()
        {
            DateTime curr = HighResolutionDateTime.UtcNow;
            notifiedCnt++;

          //  Console.WriteLine("obsver cnt" + notifiedCnt);
            if (notifiedCnt == obsvrCnt)
            {
                notifyCompleteTime = curr;
                timeCostToFinishProc = notifyCompleteTime - startTime;
                latency = timeCostToFinishProc.TotalMilliseconds;

            }
            
        }
        public ObjAction(int id, string operation, SqlGeography loc)
        {
            this._id = id;
            this._loc = loc;
            if (operation.StartsWith("n")) this._operation = Operation.Add;
            else if (operation.StartsWith("p")) this._operation = Operation.Modify;
            else if (operation.StartsWith("d")) this._operation = Operation.Delete;
            else this._operation = Operation.NA;
        }
        public int id { get { return this._id; } }
        public SqlGeography loc { get { return this._loc; } }
        public Operation operation { get { return this._operation; } }
    }

}
