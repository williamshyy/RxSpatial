using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RxSpatial;
namespace LoadTestApplication
{
      class ProcTraffic
        {
            private List<ObjAction> actionListOneTime;
            private Dictionary<int, MovingObject> movingObjList;
            private BatchLoadParams.TestCase testcase;
            public ProcTraffic(BatchLoadParams.TestCase testcase, Dictionary<int, MovingObject> movingObjList, List<ObjAction> actionListOneTime)
            {
                this.movingObjList = movingObjList;
                this.actionListOneTime = actionListOneTime;
                this.testcase = testcase;
            }
            public void proc()
            {
               

                // Boolean subscribeFlag = false;//whether it is new moving obj added or not
                //false-new , true-old point moving

                // foreach (List<ObjAction> actionListOneTime in objActionList)
                //{
                DateTime start_time = HighResolutionDateTime.UtcNow;
                foreach (ObjAction action in actionListOneTime)
                {
                    if (testcase.output == true)
                    {
                        try
                        {
                            updateNewLoc(action);
                        }
                        catch (RxSpatialOverLoadException e)
                        {
                       //     Console.WriteLine("Overload");
                            testcase.output = false;
                            return;
                        }
                    }
                    else
                        break;
                }
                DateTime end_time = HighResolutionDateTime.UtcNow;
                TimeSpan time_elapse = end_time - start_time;
                if (actionListOneTime[0].operation != Operation.Add)
                {
                    Console.WriteLine("Total time_elapse " + time_elapse.TotalMilliseconds + "ms");
                   // if (time_elapse.TotalMilliseconds < 600) //remove outliers
                    //    testcase.latencyRecorder.AvgLatency(time_elapse.TotalMilliseconds);
                }


                return;


            }

            public void updateNewLoc(ObjAction action)
            {
                if (testcase.output == true)
                {
                    //Console.WriteLine("updateNewLoc");
                    switch (action.operation)
                    {
                        case Operation.Add:
                            addItem(action);
                            break;
                        case Operation.Modify:
                            updateItem(action);
                            break;
                        case Operation.Delete:
                            delItem(action.id);
                            break;
                        default:
                            break;
                    }
                }
            }

            private void addItem(ObjAction action)
            {
                if (movingObjList.ContainsKey(action.id))
                {
                    foreach(int objActionID in movingObjList.Keys)
                        Console.WriteLine(objActionID);
                    Console.WriteLine("totally"+movingObjList.Count);
                   
                    throw new Exception("key" + action.id.ToString() + "already added");
                }

                MovingObject newItem = new MovingObject(action.id, testcase, movingObjList);

                newItem.subscribeAll();
                newItem.feedLocation(action);
                //  Console.WriteLine("add"+id.ToString());
                movingObjList.Add(action.id, newItem);

            }


            private void updateItem(ObjAction action)
            {
                if (movingObjList.ContainsKey(action.id))
                {
                    MovingObject currItem = movingObjList[action.id];
                    currItem.feedLocation(action);
                    
                }
            }

            private void delItem(int id)
            {
                if (movingObjList.ContainsKey(id))
                {
                    movingObjList[id].delObj();
                    movingObjList.Remove(id);
                }
            }
        }

    
}
