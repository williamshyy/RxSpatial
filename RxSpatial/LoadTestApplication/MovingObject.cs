using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

//using Microsoft.Maps.MapControl.WPF;

using System.IO;

using RxSpatial;

namespace LoadTestApplication
{
    class MovingObject
    {
        int id;
 

        BatchLoadParams.TestCase testcase;
        Dictionary<string, IDisposable> observerIDisposableDict = new Dictionary<string, IDisposable>();
        public RxGeography rxGeography = new RxGeography();
        private ObjMovementObserver objMovementObserver; //receive location feed to trigger visualized movement in objMovementHandler
        private Dictionary<int, MovingObject> movingObjList;//get the ptr to read other active Rx for test 
        Dictionary<string, IntersectObserver> subscribedObservers 
            = new Dictionary<string, IntersectObserver>();//for timing only
        public MovingObject(int id, BatchLoadParams.TestCase loadparam,Dictionary<int,MovingObject> movingObjList)
        {
            this.testcase = loadparam;
            this.id = id;          
            this.objMovementObserver = new ObjMovementObserver();
            this.objMovementObserver.Subscribe(rxGeography);
            this.movingObjList = movingObjList;
            
            //subscribeAll();
        }


        public void feedLocation(ObjAction objAction) 
        {
            //objAction.timerStart();
            //objAction.updateObsvrCnt(subscribedObservers.Count);
            try
            {
                rxGeography.OnNext(objAction.loc);
            }
            catch (RxSpatialOverLoadException e)
            {
                Console.WriteLine("Overload proc");
                testcase.output=false;
                throw;
            }
           /* foreach(IntersectObserver obsvr in subscribedObservers.Values)
            {
                obsvr.addAction(objAction);
            }*/
           // double tsms = ts.TotalMilliseconds;
            //Program.latencyRecorder.AvgLatency(tsms);
        }
        public void subscribeAll()
        {

            List<int> keys=new List<int>(movingObjList.Keys);
            int size=movingObjList.Count;
          //  Console.WriteLine("subscribing");
           /* Random rand =  new Random();
            for (int i = 0; i < loadparam.observerNumber; i++) {
                int randomNum = rand.Next(size);
                int randomKey = keys[randomNum];
                
                subscribeRxRxIntersect(movingObjList[randomKey].rxGeography, "RxRxIntersct" + i);
            }*/
           /* int i = 0;
            foreach (string key in SubscribeParams.polygonList.Keys)
            {
                i++;
                if (i <= loadparam.observerNumber){
                    subscribeIntersect(key, SubscribeParams.polygonList[key]);
                    }
                else
                    break;
            }*/
            if((testcase.subscribeType.ToLower()).Equals("rxintersect"))
            {
                double step = 1.0*10000/testcase.observerNumber;
                for (int i = 0; i < testcase.observerNumber; i++)
                {
                    subscribeIntersect(i.ToString(), SubscribeParams.polygonList[((int)(step*i)).ToString()]);
                }
            }

            if ((testcase.subscribeType.ToLower()).Equals("rxrxintersect"))
            {
                for (int i = 0; i < movingObjList.Count; i++)
                {
                    subscribeRxRxIntersect(movingObjList[i].rxGeography, i.ToString());
                }
            }
        /*    TextWriter stdOut = Console.Out;
            StreamWriter swHashMapGridDump = new StreamWriter(FilePaths.dataPath + "HashMapGridDump"+id+".txt");
            Console.WriteLine("subscribe done");
            Console.SetOut(swHashMapGridDump);
            Console.WriteLine(rxGeography.dumpHashMapSlots());
            Console.SetOut(stdOut);*/

           // Console.WriteLine("subscribed");
           // Console.WriteLine(subscribedObservers.Count); 
        }

        public void subscribeIntersect(string name, SqlGeography polygon)
        {
            if (!observerIDisposableDict.ContainsKey(name))
            {
              IntersectObserver o= new IntersectObserver();
               observerIDisposableDict.Add(name, 
                   rxGeography.RxIntersect(polygon, o));
               subscribedObservers.Add(name,o);
            }
            else
            {
                //Trace err
                
            }   
        }

        public void subscribeRxRxIntersect(RxGeography g,string name)
        {
            if (!observerIDisposableDict.ContainsKey(name))
            {
                IntersectObserver obsvr=new IntersectObserver();
                observerIDisposableDict.Add(name,
                    rxGeography.RxRxIntersect(g, obsvr));
                subscribedObservers.Add(name,obsvr);
            }
            else
            {
                //Trace err
            }
        }

        public void unsubscribe(string name)
        {
            if (observerIDisposableDict.ContainsKey(name))
            {
                observerIDisposableDict[name].Dispose();
            }
        }
        public void delObj()
        {
            
            var observerDisposables = observerIDisposableDict.Values;
            foreach (IDisposable observerDisposable in observerDisposables)
            {
                observerDisposable.Dispose();
            }
            observerIDisposableDict.Clear();
            this.objMovementObserver.OnCompleted();
        }
    
    }
}
