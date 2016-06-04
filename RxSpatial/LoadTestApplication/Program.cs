using System;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using System.Configuration;
using Microsoft.SqlServer.Types;


using RxSpatial;

namespace LoadTestApplication
{
    class Program
    {

        static void Main(string[] args)
        {

           
           //RxRx to start - test vehicle num


          /*  for (int time = 0; time < 9; time++)
            {
                if (time % 3 == 0)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                }
                else if (time % 3 == 2)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                }
                else if (time % 3 == 1)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                }
                Console.WriteLine("init..done.Running");
                TextWriter stdOut = Console.Out;
                StreamWriter swVm = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameVnum + time.ToString() + "rxrxVN.txt");
                 BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(-1, 20, 150, "rxrxintersect");
                 do
                 {
                    
                     Console.SetOut(stdOut);
                     Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                  + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);

                     testVechicleNum(testcase);//return testcase , return it with vehicle number threshold found
                     Console.SetOut(swVm);
                     Console.WriteLine(testcase.vehicleNumber);
                     Console.SetOut(stdOut);
                     swVm.Flush();
                     testcase.updateTimeInterval = testcase.updateTimeInterval + 150;
                 } while (testcase.updateTimeInterval <= 750);
            }*/
            //RxRx to start- test delay
            for (int time = 0; time < 10; time++)
            {
                string str="";
                if (time%2  == 0)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                    str = "r";
                }
              //  else if (time%3  == 2)
               // {
                   // ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                //}
                else if (time%2  == 1)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                    str = "rum";
                }
                Console.WriteLine("init...");
                SubscribeParams.init();
                // LoadParams.init();
               // BatchLoadParams.init();
               // XmlDocument doc = new XmlDocument();

                Console.WriteLine("init..done.Running");
                TextWriter stdOut = Console.Out;
                StreamWriter swIntvl = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameInterval
                    +"rxrxIntv" + time.ToString() + str+"rxrx12001400.txt");
 
                BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(-1, 1200, 2000, "rxrxintersect");
                do
                {
                    Console.SetOut(stdOut);
                    Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                 + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
                    testcase.output = true;
                    Simulation s = new Simulation(testcase);
                    s.checkThreadsStop();
                    
                    Console.SetOut(swIntvl);
                    Console.WriteLine(testcase.latency);
                    Console.SetOut(stdOut);
                     swIntvl.Flush();
                     testcase.vehicleNumber = testcase.vehicleNumber + 200;
                    testcase.latencyRecorder = new AvgCalc();
                } while (testcase.vehicleNumber <= 1400);
 
               // swMaxIntvl.Close();
                swIntvl.Close();
                //swVm.Close();

            }
            

    
                                // LoadParams.init();
                     //           BatchLoadParams.init();
                              //  XmlDocument doc = new XmlDocument();
                               // doc.Load(FilePaths.dataPath + FilePaths.batchLoadTestParamsXml);
                               // XmlNodeList tcList = doc.GetElementsByTagName("Testcase");
                               // foreach (XmlNode tc in tcList)
                               // {
                                 //   Console.WriteLine(tc.SelectSingleNode("Interval").InnerText);
                               // }

                                
                              //  StreamWriter swMaxIntvl = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputMaxLatencyFilenameInterval + time.ToString() + ".txt");
                               // StreamWriter swIntvl = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameInterval + time.ToString() + ".txt");
                             
                                // Interval 50 to 300, observer 500
                                 
                                //StreamWriter swIntvl = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameInterval + time.ToString() + ".txt");
                              /*  BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(1000, 1, 1000,"rxintersect");
                                do
                                {
                                    Console.SetOut(stdOut);
                                    Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                                 + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
                                    testcase.output = true;
                                    Simulation s = new Simulation(testcase);
                                    s.checkThreadsStop();

                                    Console.SetOut(swIntvl);
                                    Console.WriteLine(testcase.latency);
                                    Console.SetOut(swMaxIntvl);
                                    Console.WriteLine(testcase.maxLatency);
                                    Console.SetOut(stdOut);
                                    //Console.WriteLine(testcase.latency);
                                    //Console.SetOut(stdOut);
                                    // swIntvl.Flush();
                                    swIntvl.Flush();
                                    swMaxIntvl.Flush();
                                    testcase.observerNumber = testcase.observerNumber + 1000;
                                    testcase.latencyRecorder = new AvgCalc();
                                } while (testcase.observerNumber <= 40000);
                                // observer 1000 to 20 , Interval 100

                                /*BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(10000, 2, 50);
                                 do
                                 {
                                     Console.SetOut(stdOut);
                                     Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                                  + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
                                     if (testcase.observerNumber == 10000)
                                     {
                                         for (int x = 0; x < 3; x++)
                                         {
                                             Console.WriteLine(x);
                                             lock (testcase)
                                             {
                                                 testVechicleNum(testcase);//return testcase , return it with vehicle number threshold found
                                             }
                                             }
                                     }
                                     else
                                         testVechicleNum(testcase);
                                      //   Console.WriteLine("Test Next: observer#:" + testcase.observerNumber
                                // + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);

                                     Console.SetOut(swVm);
                                     Console.WriteLine(testcase.vehicleNumber);
                                     Console.SetOut(stdOut);
                                     //Console.WriteLine(testcase.latency);
                                     //Console.SetOut(stdOut);
                                    // swIntvl.Flush();
                                     swVm.Flush();
                                     testcase.observerNumber = testcase.observerNumber-500;
                                     testcase.latencyRecorder = new AvgCalc();
                                 } while (testcase.observerNumber > 500);

                                 swVm.Close();

                                /*  foreach (List<BatchLoadParams.TestCase> l in BatchLoadParams.batchLoadList)
                                {
                                    //Console.SetOut(sw);
                                    Console.WriteLine(l.First().updateTimeInterval+"TimeInterval");
                                  //  Console.SetOut(stdOut);
                                    foreach (BatchLoadParams.TestCase testcase in l)
                                    {
                                        testcase.output = true;
                                        while (testcase.output==true)
                                        {
                                            runTest(testcase, 0);
                                        }

                    
                                        runTest(testcase, 0);
                                        // sw.Flush();
                                        if (testcase.output == false)
                                        {
                                            Thread.Sleep(5000);
                                        }
                                        // testcase.latency = latencyRecorder.Avg;
                                        Console.SetOut(sw);
                                        //  Console.WriteLine("Time:" + testcase.ts.ToString() + " ObserverNum:" + testcase.observerNumber
                                        //     + " VehicleNum:" + testcase.vehicleNumber + " Interval:" + testcase.updateTimeInterval
                                        //   + " PassStatus:" + testcase.output + "AvgLatency:" + testcase.latency);
                                        Console.WriteLine(testcase.latency);
                                        sw.Flush();
                                        Console.SetOut(stdOut);
                                    }
                                }
                                swMaxIntvl.Close();
                                swIntvl.Close();
                                swVm.Close();

                            }*/

          /*  for (int time = 0; time < 12; time++)
             {
                 if (time % 4 == 3)
                                {
                       ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                                }
                                else if (time % 4 == 1)
                                {
                                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                                }
                                else if (time % 4 == 2)
                                {
                                    ConfigurationManager.AppSettings["preferredDataStruct"] = "hashMap";
                                }
                                else if (time % 4 == 0)
                                {
                                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                                }
                                Console.WriteLine("init...");
                                SubscribeParams.init();
                                 Console.WriteLine("init..done.Running");
                                TextWriter stdOut = Console.Out;
                  BatchLoadParams.TestCase testcase;
                
                  if (time % 4 == 3)
                  {
                      testcase = new BatchLoadParams.TestCase(100, 165, 750, "rxintersect");
                     // ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                  }
                  else if (time % 4 == 1)
                  {
                      testcase = new BatchLoadParams.TestCase(100, 240, 750, "rxintersect");
                      // ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                  }
                  else if (time % 4 == 2)
                  {
                      testcase = new BatchLoadParams.TestCase(100, 240, 750, "rxintersect");
                      // ConfigurationManager.AppSettings["preferredDataStruct"] = "hashMap";
                  }
                  else //if (time % 4 == 0)
                  {

                      testcase = new BatchLoadParams.TestCase(100, 240, 750, "rxintersect");
                      //ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                  }
                
                
                StreamWriter swVm = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameVnum + time.ToString() + "rxrxVN.txt");

                 do
                 {

                     Console.SetOut(stdOut);
                     Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                     + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);


                     testVechicleNum(testcase);//return testcase , return it with vehicle number threshold found
                     Console.SetOut(swVm);
                     Console.WriteLine(testcase.vehicleNumber);
                     Console.SetOut(stdOut);
                     swVm.Flush();


                     testcase.updateTimeInterval = testcase.updateTimeInterval + 150;
                 } while (testcase.updateTimeInterval <= 1500);

                         swVm.Close();
 
                        }*/
/*            for (int time = 0; time < 12; time++)
            {
                if (time % 4 == 0)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                }
                else if (time % 4 == 2)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                }
                else if (time % 4 == 1)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "hashMap";
                }
                else if (time % 4 == 3)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                }
                Console.WriteLine("init...");
                SubscribeParams.init();
                Console.WriteLine("init..done.Running");
                TextWriter stdOut = Console.Out;
                BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(150, 10, 200, "rxintersect");
                    

                StreamWriter swVm = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameVnum + time.ToString() + "rxrxVN.txt");

                do
                {

                    Console.SetOut(stdOut);
                    Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                    + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);


                    testVechicleNum(testcase);//return testcase , return it with vehicle number threshold found
                    Console.SetOut(swVm);
                    Console.WriteLine(testcase.vehicleNumber);
                    Console.SetOut(stdOut);
                    swVm.Flush();


                    testcase.observerNumber = testcase.observerNumber - 30;
                } while (testcase.observerNumber >= 30);

                swVm.Close();
            }*/
            /*
            for (int time = 0; time < 12; time++)
            {
                if (time % 4 == 0)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "list";
                }
                else if (time % 4 == 2)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RTREE";
                }
                else if (time % 4 == 1)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "hashMap";
                }
                else if (time % 4 == 3)
                {
                    ConfigurationManager.AppSettings["preferredDataStruct"] = "RUMTREE";
                }
                Console.WriteLine("init...");
                SubscribeParams.init();
                Console.WriteLine("init..done.Running");
                TextWriter stdOut = Console.Out;
                BatchLoadParams.TestCase testcase = new BatchLoadParams.TestCase(2000, 10, 100, "rxintersect");


                StreamWriter swIntvl = new StreamWriter(FilePaths.dataPath + FilePaths.batchLoadTestOutputFilenameInterval + "rx" + time.ToString() + "rxrxVN.txt");

                do
                {

                    Console.SetOut(stdOut);
                    Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                 + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
                    testcase.output = true;
                    Simulation s = new Simulation(testcase);
                    s.checkThreadsStop();

                    Console.SetOut(swIntvl);
                    Console.WriteLine(testcase.latency);
                    //Console.SetOut(swMaxIntvl);
                    // Console.WriteLine(testcase.maxLatency);
                    Console.SetOut(stdOut);
                    //Console.WriteLine(testcase.latency);
                    //Console.SetOut(stdOut);
                    swIntvl.Flush();
                    //   swIntvl.Flush();
                    //  swMaxIntvl.Flush();
                    //testcase.vehicleNumber = testcase.vehicleNumber + 400;
                    testcase.latencyRecorder = new AvgCalc();
                    testcase.updateTimeInterval = testcase.updateTimeInterval + 100;
                  //  testcase.observerNumber = testcase.observerNumber + 2000;
                } while (testcase.updateTimeInterval <= 800);
                    // while (testcase.observerNumber <= 14000);
                
                swIntvl.Close();
            }*/
            Console.ReadLine();
        }

        private static void testVechicleNum(BatchLoadParams.TestCase testcase)
        {

          
            testcase.output = true;
            while (testcase.output == true)
            {

            

     
            Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
            DateTime start = HighResolutionDateTime.UtcNow;
            try
            {
                Simulation s = new Simulation(testcase);
                s.checkThreadsStop();
            }
            catch (RxSpatialOverLoadException e)
            {
                Console.WriteLine("Overload proc");
                testcase.output = false;
                return;
            }
            if (testcase.output == true)
            {
                testcase.ts = HighResolutionDateTime.UtcNow - start;
                Console.WriteLine("Test end: time" + testcase.ts);
                testcase.vehicleNumber += 5; 
            }
            }

            }
        }

      /*  private static void runTest(BatchLoadParams.TestCase testcase, int retryCnt)
        {
            Simulation s = new Simulation(testcase);
            //s.readFile();
            s.generateTraffic();
            Console.WriteLine("Test start: observer#:" + testcase.observerNumber
                + " vehicle#:" + testcase.vehicleNumber + " timeInterval:" + testcase.updateTimeInterval);
           // latencyRecorder = new AvgCalc();
            DateTime start =  HighResolutionDateTime.UtcNow;
            try
            {
                s.procTraffic();
                testcase.ts =  HighResolutionDateTime.UtcNow - start;
                testcase.output = true;
              //  Console.WriteLine("");
                //Console.WriteLine("======done======");
                testcase.vehicleNumber += 2; // re test this case
                runTest(testcase, 0);
            }
            catch (RxSpatialOverLoadException e)
            {
                if (retryCnt < 1)
                    runTest(testcase,  retryCnt + 1);
                else
                {
                  //  Console.WriteLine("..RxSpatialOverLoadException.");
                    testcase.ts =  HighResolutionDateTime.UtcNow - start;
                    testcase.output = false;
                    testcase.vehicleNumber -= 3;
                }
            }
        }
       }*/


    class Simulation
    {

        BatchLoadParams.TestCase testcase;
        Boolean stopIfFail=true;
        Dictionary<int, MovingObject> movingObjList = new Dictionary<int, MovingObject>();
        List<ObjAction>[] objActionList = new List<ObjAction>[10];
        public static System.Timers.Timer timer = new System.Timers.Timer();
        AvgCalc latencyCal = new AvgCalc();
        TextReader tr = new StreamReader(FilePaths.dataPath + FilePaths.trafficFilename);
   
     //   Thread[] threads = new Thread[40];
        public Boolean stopflag=false;
     //   private static readonly object syncLock = new object();
        public Simulation(BatchLoadParams.TestCase testcase)
        {
            
                this.testcase = testcase;
                tr.ReadLine();
                tr.ReadLine();
                generateTrafficData();
                callProcTraffic();//init moving objects
                scheduleDataFeed();
        }
        public void scheduleDataFeed()
        {
            Console.WriteLine("Timer start ");
            //  Timer setup
            timer.Elapsed += this.updateTraffic;
            timer.Interval = testcase.updateTimeInterval;
            timer.Enabled = true;
        }
        private void updateTraffic(object sender, EventArgs e)
        {
            idx++;
          //  Console.WriteLine("Timer tick:idx="+idx);
            if ((testcase.output == false && stopIfFail == true) || idx >= objActionList.Length)
            {
                Console.WriteLine("testcase.output:" + testcase.output+
                    "idx"+idx);
                timer.Stop();
                stopflag = true;
                timer.Enabled = false;
                timer.Elapsed -= this.updateTraffic;
                //testcase.latency = testcase.latencyRecorder.Avg;
                return;
            }
            callProcTraffic();

            if (idx >= objActionList.Length)
            {
           //     Console.WriteLine(" stop");
                stopflag = true;
            }
        }

        public void generateTrafficData()
        {

            for (int i = 0; i < 10; i++)
            {
                List<ObjAction> actionListOneTime = new List<ObjAction>();
                while (actionListOneTime.Count < testcase.vehicleNumber)
                {
                    actionListOneTime.Add(new ObjAction(actionListOneTime.Count,
                    i == 0 ? "newpoint" : "point", getLocation(SqlGeography.Point(47.5, -122.0, 4326), 166500)));
                }
                objActionList[i]=actionListOneTime;
            }
            
        }
        private SqlGeography getLocation(SqlGeography g0, int radius)
        {
            double x0 = (double)(g0.Long);
            double y0 = (double)(g0.Lat);
            Random random = new Random();
            double radiusInDegrees = radius / 111000f;
            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(y0);

            // Set the adjusted location
            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;

            return SqlGeography.Point(foundLatitude, foundLongitude, 4326).STBuffer(500.0);
        }
        int idx = 0;
        public void callProcTraffic()
        
        {
            if (idx >= objActionList.Length)
            {
                Console.WriteLine("callProcTraffic,idx" + idx+",stop");
                stopflag = true;
                return;
            }
            //Console.WriteLine("callProcTrafficByIdx"+idx);
            ProcTraffic procTrafficOneTime = new ProcTraffic(testcase, movingObjList, objActionList[idx]);
          /*  if (idx > 0)
            {
                lock (procTrafficOneTime)
                {
                    threads[idx] = new Thread(procTrafficOneTime.proc);
                    threads[idx].Start();
                }
                if (idx == 39)
                {
                    stopflag = true;
                   // checkThreadsStop();
                }
            }
            else
            {*/

                procTrafficOneTime.proc();

              //  Console.WriteLine("idx" + idx);


            //}
          //  if(idx<39)
            //idx++;
            

        }

        public void checkThreadsStop()
        {
           // lock (this)
            //{
                Console.WriteLine("check stop");
                while (stopflag == false)
                {
                    Thread.Sleep(10);
                }
                double maxLatency = 0;
                foreach (MovingObject o in movingObjList.Values)
                {
                    while (o.rxGeography.latency == 0)
                        Thread.Sleep(10);
                    this.latencyCal.AvgLatency(o.rxGeography.latency);
                    maxLatency = maxLatency > o.rxGeography.latency ? maxLatency : o.rxGeography.latency;
                }
                testcase.latency=this.latencyCal.Avg;
                testcase.maxLatency = maxLatency;
               /* for (int i = 0; i < 40; i++)
                {
                    while (threads[i] != null && threads[i].IsAlive == true)
                    {
                        Console.WriteLine("alive");
                        Thread.Sleep(10);
                    }
                   // threads[i] = null;
                }
                Console.WriteLine("Clear");*/


               // dataFeed = null;
                movingObjList.Clear();
         //   }
        }
    }
}
