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
    class IntersectObserver:IObserver<SqlGeography>
    {
        private IDisposable unsubscriber;
        private IntersectHandler handler;
        private List<SqlGeography> polygonList;
        private int id;
        public IntersectObserver(List<SqlGeography> polygonList, Map baseMap, int id)
        {
            this.id = id;
            this.polygonList = polygonList;
            handler = new IntersectHandler(baseMap,id);
        }

        public int Id
        { get { return this.id; } }

        public virtual void Subscribe(IObservable<SqlGeography> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {

             handler.del();
             this.Unsubscribe();
        }

       
        public virtual void OnError(Exception e)
        {

        }

        public virtual void OnNext(SqlGeography loc)
        {
            Boolean isIntersect = checkIntersect(loc);
            handler.proc(loc,isIntersect);
        }

        private Boolean checkIntersect(SqlGeography loc)
        {
            foreach (SqlGeography polygon in polygonList)
                if (polygon.STIntersects(loc).IsTrue)
                    return true;
            return false;
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
    }

