using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApplication1
{

public class SimpleThread
{
     public void Method()
     {

          for (int i = 0; i < 10; i++)
          {
              Console.WriteLine("ThreadProc:{0}", i);
              Thread.Sleep(10);
          }
     }  
     static void Main()
     {
          SimpleThread thread1 = new SimpleThread();

          //thread1.Method();
          ThreadStart ts = new ThreadStart(thread1.Method);
          Thread t = new Thread(thread1.Method);
          Thread t1 = new Thread(thread1.Method);
         t.Start();
         t1.Start();
         //t.Join();
          //thread1.Method();
          for (int i = 0; i < 4; i++)
          {
              Console.WriteLine("Main thread:Do some work.");
             Thread.Sleep(10);
          }
         // t.Join();
          Console.ReadLine();
      }

    }
}

/*
using System;
using System.Threading;

public class ThreadExample
{
       public static void ThreadProc()
       {
              for(int i = 0; i <10; i++)
              {
                  Console.WriteLine("ThreadProc:{0}",i);
                  Thread.Sleep(0);
              }
        }
        public static void Main()
        {
               Console.WriteLine("Main thread: Start a second thread.");
               Thread t =new Thread(new ThreadStart(ThreadProc));
               t.Start();
               for(int i = 0; i < 4; i++)
               {
                    Console.WriteLine("Main thread:Do some work.");
                    Thread.Sleep(0);
               }
                Console.WriteLine("Main thread:Call Join(),to wait until ThreadProc ends.");
                t.Join();
                Console.WriteLine("Main thread:ThreadProc.Join has returned.Press Enter to end program.");
                Console.ReadLine();
         }
}*/
