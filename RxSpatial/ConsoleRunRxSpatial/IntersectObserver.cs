using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunRxSpatial
{
    class IntersectObserver : IObserver<bool>
    {
        public virtual void OnNext(bool b)
        {
            Console.WriteLine("The intersection status is:{0}", b.ToString());
        }
        public virtual void OnCompleted()
        {
        }
        public virtual void OnError(Exception e)
        {
        }
    }
}
