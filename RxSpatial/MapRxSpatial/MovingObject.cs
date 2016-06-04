using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maps.MapControl.WPF;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

using System.Windows.Media;

using RxSpatial;

namespace MapRxSpatial
{
    class MovingObject
    {
        int id;
        private RxGeography locObserver;
        private IntersectObserver intersectObserver;

        public MovingObject(int id, RxGeography locObserver)
        {
            this.id = id;
            this.locObserver = locObserver;
        }

        public void feedLocation(SqlGeography location)
        {
            locObserver.OnNext(location);
        }

        public void setIntersectObserver(IntersectObserver intersectObserver)
        {
            this.intersectObserver = intersectObserver;
            this.intersectObserver.Subscribe(locObserver);
           // this.locObserver.Subscribe(intersectObserver);
        }
        public void delObj()
        {
            this.intersectObserver.Unsubscribe();
            this.intersectObserver.OnCompleted();
            this.locObserver.OnCompleted();
        }

        public void delIntersectObserver()
        {
            this.intersectObserver.Unsubscribe();
        }

        public int Id
        { get { return this.id; } }


    }
}
