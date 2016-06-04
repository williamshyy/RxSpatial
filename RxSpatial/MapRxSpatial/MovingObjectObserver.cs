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
    class MovingObjectObserver:IObserver<SqlGeography>
    {

        private IDisposable unsubscriber;
        private MovingObjHandler handler;
        public MovingObjectObserver(MovingObjHandler handler)
        {
             this.handler=handler;
        }
        public virtual void OnNext(SqlGeography loc)
        {
            handler.move(loc);
        }

        public virtual void OnCompleted()
        {
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {

        }

        public virtual void Subscribe(IObservable<SqlGeography> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
            handler.del();
        }
    }
}
