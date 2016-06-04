using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SqlServer.Types;

namespace ConsoleRunRxSpatial
{
    public class LocationObserver : IObserver<SqlGeography>
    {
        private IDisposable unsubscriber;
        private string instName;

        public LocationObserver(string name)
        {
            this.instName = name;
        }

        public string Name
        { get { return this.instName; } }

        public virtual void Subscribe(IObservable<SqlGeography> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(SqlGeography value)
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", (double) (value.Lat), (double) (value.Long), this.Name);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}