using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maps.MapControl.WPF;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

using System.Windows.Media;

namespace MapRxSpatial
{
    class MovingObjHandler
    {
        protected Pushpin pushpin;
        public Object id;
        public Map baseMap;
        public MovingObjHandler(Object id,Map baseMap)
        {
            this.baseMap = baseMap;
            this.id = id;
            this.pushpin = new Pushpin();
            this.pushpin.Content = id;
        }

        public void move(SqlGeography newLoc,Object content,Brush background)
        {
            //this.pushpin = new Pushpin();
            this.pushpin.Content = content;
            this.pushpin.Location = new Location(newLoc.Lat.Value, newLoc.Long.Value);
            this.pushpin.Background = background;

            baseMap.Children.Remove(this.pushpin);
            baseMap.Children.Add(this.pushpin);
            
        }

        public void updatePushpinId(Object id)
        {
            this.pushpin.Content = id;
        }
        public void updatePushpinColor(Brush color)
        {
            this.pushpin.Background = color;
        }
        public void move(SqlGeography newLoc)
        {
            baseMap.Children.Remove(this.pushpin);
            this.pushpin.Location = new Location(newLoc.Lat.Value, newLoc.Long.Value);

            baseMap.Children.Add(this.pushpin);
        }

        public void del()
        {
            baseMap.Children.Remove(this.pushpin);
        }

        ~MovingObjHandler()
        {
           // baseMap.Children.Remove(this.pushpin);
        }

    }
}
