using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

using RxSpatial;

namespace LoadTestApplication
{
    class ObjMovementObserver:IObserver<SqlGeography>
    {

        private IDisposable unsubscriber;
        public ObjMovementObserver()
        {
        }
        public virtual void OnNext(SqlGeography loc)
        {
           //Console.WriteLine("new loc"+loc.ToString());
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
        }
    }
}
