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
    class MovingObj
    {
        int id;
        MovingObjHandler handler;
        private MovingObjectObserver locObserver;
        private List<Observer> observers = new  List<Observer>();
        RxGeography newLocation = new RxGeography();
        public MovingObj(int id, Map baseMap)
        {
            this.id = id;          
            this.handler=new MovingObjHandler(id,baseMap);
            this.locObserver = new MovingObjectObserver(handler);
            this.locObserver.Subscribe(newLocation);
        }


        public void feedLocation(SqlGeography location)
        {
            newLocation.OnNext(location);
        }

        public void addIntersectObserver(string name, List<SqlGeography> polygonList)
        {
            IntersctObserver intersectObserver = new IntersctObserver(name,polygonList,handler);
            intersectObserver.Subscribe(newLocation);
            //Put the locObserver to the end so that every observer could update pushpin info before actually moved;
            locObserver.OnCompleted();
            locObserver.Subscribe(newLocation);
            observers.Add(intersectObserver);
        }

        public void delIntersectObserver(string name)
        {
            foreach (Observer observer in observers)
            {
                if (observer.Type.Equals(Observer.ObserverType.Intersect) && observer.Name.Equals(name))
                {
                    observer.OnCompleted();
                    observers.Remove(observer);
                    return;
                }
            }
        }
        public void delObj()
        {
            foreach (Observer observer in observers)
            {
                observer.OnCompleted();
            }
            observers.Clear();
            this.locObserver.OnCompleted();
        }
    }
}
