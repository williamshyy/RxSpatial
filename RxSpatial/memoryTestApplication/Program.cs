using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RxSpatial;

namespace memoryTestApplication
{

    class Program
    {
        static void Main(string[] args)
        {

         //  Object obj=new Object();
           Dictionary<Object, RTree.Rectangle> dict0 = new Dictionary<Object, RTree.Rectangle>();
 
          RTree.RTree<Object> t = new RTree.RTree<Object>();

           Object[] obj = new Object[200];
           for (int i = 0; i < 200; i++)
               obj[i] = new Object();

           for (int i = 0; i < 50; i++)
           {
          //     Console.WriteLine("MemUsage:" + GC.GetTotalMemory(true));
               RTree.Rectangle rect = new RTree.Rectangle(i, i, i + 1, i + 1);

             /*  if (dict0.ContainsKey(obj))
               {
                   t.Delete(dict0[obj], obj);
                   dict0.Remove(obj);
               }*/
               t.Add(rect, obj[i]);
               dict0.Add(obj[i], rect);


            //   Console.WriteLine("Treesize:" + t.Count);
           }

           for (int i = 0; i < 50; i++)
           {
                if (dict0.ContainsKey(obj[i]))
                 {
                     t.Delete(dict0[obj[i]], obj[i]);
                     dict0.Remove(obj[i]);
                 }
           }

           for (int i = 50; i < 200; i++)
           {
               //     Console.WriteLine("MemUsage:" + GC.GetTotalMemory(true));
               RTree.Rectangle rect = new RTree.Rectangle(i, i, i + 1, i + 1);

               /*  if (dict0.ContainsKey(obj))
                 {
                     t.Delete(dict0[obj], obj);
                     dict0.Remove(obj);
                 }*/
               t.Add(rect, obj[i]);
               dict0.Add(obj[i], rect);


               //   Console.WriteLine("Treesize:" + t.Count);
           }

            

           Console.WriteLine("RTree-MemUsage:"+GC.GetTotalMemory(true));
          // t.Add(new RTree.Rectangle(0,0,0,0), obj);
            
           RUMTree.RUMTree<Object> RUMt = new RUMTree.RUMTree<Object>();

           Dictionary<Object, RUMTree.Rectangle> dict = new Dictionary<Object, RUMTree.Rectangle>();
            Object obj1=new Object();
           for (int i = 0; i < 200; i++)
           {

               RUMTree.Rectangle rect=new RUMTree.Rectangle(i, i, i + 1, i + 1);

               if (dict.ContainsKey(obj1)) {
                   RUMt.Delete(dict[obj1], obj1);
                   dict.Remove(obj1); }
               RUMt.Add(rect, obj1);
               dict.Add(obj1, rect);
              // if(i%20==0)
               {
               Console.WriteLine(RUMt.OIDsToUM.Count+" MemUsage:" + GC.GetTotalMemory(true));
               Console.WriteLine("Treesize:" + RUMt.Count);
               }
           }


           Console.WriteLine("MemUsage:" + GC.GetTotalMemory(true));
             
           Console.ReadLine();
        }
    }
}
