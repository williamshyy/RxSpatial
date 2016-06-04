using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;


using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;

namespace MapRxSpatial
{
    class IntersctObserver : Observer
    {
        private List<SqlGeography> polygonList;
        //private Boolean isIntersect=false;
        private string name;
        MovingObjHandler handler;
        public IntersctObserver(String name, List<SqlGeography> polygonList, MovingObjHandler handler)
        {
            this.handler = handler;
            this.polygonList = polygonList;
            setObserverType(ObserverType.Intersect);
            setName(name);
        }

        

       // public Boolean IsIntersect
        //{ get { return this.isIntersect; } }

        override public void OnNext(SqlGeography loc)
        {
            Boolean isIntersect = checkIntersect(loc);
            if (isIntersect)
                handler.updatePushpinColor(Brushes.Red);
            else
                handler.updatePushpinColor(Brushes.Blue);
        }

        private Boolean checkIntersect(SqlGeography loc)
        {
            foreach (SqlGeography polygon in polygonList)
                if (polygon.STIntersects(loc).IsTrue)
                    return true;
            return false;
        }
    }
}

