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
    
    class MovingObjectHandler
    {
        public Map baseMap;
        Dictionary<int, Pushpin> movingObjectSet=new  Dictionary<int, Pushpin> ();
        //Dictionary<int, Location> movingOb
        TextReader tr = new StreamReader(Parameters.dataPath + Parameters.trafficFilename);
        string line;
        public static DispatcherTimer dispatcherTimer;
       public MovingObjectHandler(Map map)
        {
            baseMap = map;
            tr.ReadLine();
             tr.ReadLine();
             line=tr.ReadLine();

           
         }

        public void procTraffic()
        {

              
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
             RxGeography myLocation = new RxGeography();

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
                 int currTimestamp=timestamp;
                string status = strs[2];
                double latitude = Convert.ToDouble(strs[3]);
                double longitude = Convert.ToDouble(strs[4]);
                SqlGeography location = SqlGeography.Point(latitude, longitude, 4326);
                
                while (currTimestamp == timestamp  && line!=null)
                {
                    try
                    {
                        updateNewLoc(id,status,location);
                       //updateSingleObject(id, status, latitude, longitude);
                        line = tr.ReadLine();
                        strs = line.Split();
                        id = Convert.ToInt32(strs[0]);
                        timestamp = Convert.ToInt32(strs[1]);
                        status = strs[2];
                        latitude = Convert.ToDouble(strs[3]);
                        longitude = Convert.ToDouble(strs[4]);
                        location = SqlGeography.Point(latitude, longitude, 4326);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }

            }
                        
                       
            

        }


        #region RxGeoDomain
        private void addPushPin(int id,SqlGeography loc)
        {
            
            Pushpin pin = new Pushpin();
            pin.Content = id;
            pin.Location = new Location(loc.Lat.Value, loc.Long.Value);
            baseMap.Children.Add(pin);
            movingObjectSet.Add(id, pin);
        }
        private void movePushPin(int id, SqlGeography loc)
        {
            if (!movingObjectSet.ContainsKey(id))
            {
                addPushPin(id, loc);
            }
            else
            {
                Pushpin pin = movingObjectSet[id];
                pin.Background = Brushes.Red;
                pin.Content = id;
                baseMap.Children.Remove(pin);
                pin.Location = new Location(loc.Lat.Value, loc.Long.Value);
                baseMap.Children.Add(pin);
            }
        }
        private void updateNewLoc(int id, string status, SqlGeography loc)
        {

            if (status.StartsWith("n"))
            {
                addPushPin(id, loc);
            }
            else if (status.StartsWith("p"))
            {
                movePushPin(id, loc);
            }
            else if (status.StartsWith("d"))
            {
                delPushpin(id);
            }

        }
        
        #endregion

        #region InputFileDomain
        private void updateSingleObject(int id, string status, double latitude,double longitude)
        {

            if (status.StartsWith("n"))
            {
                addPushPin(id, latitude, longitude);
            }
            else if (status.StartsWith("p"))
            {
                movePushPin(id, latitude, longitude);
            }
            else if (status.StartsWith("d"))
            {
                delPushpin(id);
            }

        }
        private void addPushPin(int id, double latitude, double longitude)
        {
            Pushpin pin = new Pushpin();
            pin.Content = id;
            pin.Location = new Location(latitude, longitude);
            baseMap.Children.Add(pin);
            movingObjectSet.Add(id, pin);

        }
        private void movePushPin(int id, double latitude, double longitude)
        {

            if (!movingObjectSet.ContainsKey(id))
            {
                addPushPin(id,latitude,longitude);
            }
            else
            {
                Pushpin pin = movingObjectSet[id];
                pin.Background = Brushes.Red;
                pin.Content = id;
                baseMap.Children.Remove(pin);
                pin.Location = new Location(latitude, longitude);
                baseMap.Children.Add(pin);
            }
          
        }
        private void delPushpin(int id)
        {
            if (movingObjectSet.ContainsKey(id))
            {
                baseMap.Children.Remove(movingObjectSet[id]);
                movingObjectSet.Remove(id);
            }

        }
        #endregion
    }
}
