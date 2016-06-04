using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

using Microsoft.SqlServer.Types;

using RxSpatial;

namespace MapRxSpatial
{

    class SimulationFromText
    {
       // Dictionary<int, MovingObject> itemList = new Dictionary<int, MovingObject>();
        Dictionary<int, MovingObj> itemList = new Dictionary<int, MovingObj>();

        public Map baseMap;
        Dictionary<int, MovingObjHandler> movingObjectSet = new Dictionary<int, MovingObjHandler>();
        //Dictionary<int, Location> movingOb
        TextReader tr = new StreamReader(Parameters.dataPath + Parameters.trafficFilename);
        string line;
        public static DispatcherTimer dispatcherTimer;
        public SimulationFromText(Map map)
        {
            baseMap = map;
            tr.ReadLine();
            tr.ReadLine();
            line = tr.ReadLine();
        }

        public void procTraffic()
        {
            //  DispatcherTimer setup
         //   MessageBox.Show("loadobj file");
            //  DispatcherTimer setup
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(updateTraffic);
            //dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(Parameters.objectsSpeedOnScreenInMS);
            dispatcherTimer.Start();
        }
        private void updateTraffic(object sender, EventArgs e)
        {
         
            if (line == null) // end of file
            {
                tr.Close();
                dispatcherTimer.Stop();
                MessageBox.Show("End of moving objects file");
                return;
            }

            if (line != null)
            {
                string[] strs = line.Split();
                int id = Convert.ToInt32(strs[0]);
                int timestamp = Convert.ToInt32(strs[1]);
                int currTimestamp = timestamp;
                string status = strs[2];
                double latitude = Convert.ToDouble(strs[3]);
                double longitude = Convert.ToDouble(strs[4]);
                SqlGeography location = SqlGeography.Point(latitude, longitude, 4326);

                while (currTimestamp == timestamp && line != null)
                {
                   // try
                    //{
                        updateNewLoc(id, status, location);
                        //updateSingleObject(id, status, latitude, longitude);
                        line = tr.ReadLine();
                        if (line == null)
                            continue;
                        strs = line.Split();
                        if (strs.Length != 5)
                            continue;
                       // MessageBox.Show(line); 
                        id = Convert.ToInt32(strs[0]);
                        timestamp = Convert.ToInt32(strs[1]);
                        status = strs[2];
                        latitude = Convert.ToDouble(strs[3]);
                        longitude = Convert.ToDouble(strs[4]);
                        location = SqlGeography.Point(latitude, longitude, 4326);
                  //  }
                   // catch (Exception ex)
                    //{
                      //  continue;
                    //}
                }

            }




        }


        #region RxGeoDomain
        private void addItem(int id, SqlGeography loc)
        {
            if (itemList.ContainsKey(id))
            {
                throw new Exception("key"+id.ToString()+"already added");
            }
           // MovingObject newItem = new MovingObject(id,new RxGeography());
            MovingObj newItem = new MovingObj(id, baseMap);

            itemList.Add(id, newItem);
            newItem.addIntersectObserver("standardPolygon",IntersectMgmt.polygonList);
            newItem.feedLocation(loc);
        }

        private void updateItem(int id, SqlGeography loc)
        {
            if (itemList.ContainsKey(id))
            {
                //MovingObject currItem = itemList[id];
                MovingObj currItem = itemList[id];
                currItem.feedLocation(loc);
            }
        }

        private void delItem(int id)
        {
           // MessageBox.Show("rm"+id.ToString());
            if (itemList.ContainsKey(id))
            {
                itemList[id].delObj();
                itemList.Remove(id);
            }

        }
        private void updateNewLoc(int id, string status, SqlGeography loc)
        {


            if (status.StartsWith("n"))
            {
                addItem(id, loc);
            }
            else if (status.StartsWith("p"))
            {
                updateItem(id, loc);
            }
            else if (status.StartsWith("d"))
            {
                delItem(id);
            }

        }

        #endregion
 
    }
}
