using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestApplication
{
    class IntersectObserver : IObserver<bool>
    {
        //private List<ObjAction> actionList=new List<ObjAction> () ;
        public IntersectObserver():base()
        {

        }

       // public void addAction(ObjAction action)
        //{
           // actionList.Add(action);
        //}
        //Need to count time here
        public  void OnNext(bool b)
        {
         //  ObjAction action=actionList.First();
           // action.updateTimeInterval();
            //actionList.Remove(action);
            //if(b==true)
          //  Console.WriteLine("Intersect status:{0}",b.ToString());
        }
        public virtual void OnCompleted()
        {
        }
        public virtual void OnError(Exception e)
        {
        }
    }
}
