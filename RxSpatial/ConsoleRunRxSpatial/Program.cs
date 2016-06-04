using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RxSpatial;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace ConsoleRunRxSpatial
{
    class Program
    {
        static RxGeography myLocation = new RxGeography();

        static void Main(string[] args)
        {
            var disposable = myLocation.Subscribe(new LocationObserver("Mohamed's Location"));
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => FeedLocation()));
            Task.WaitAll(tasks.ToArray());   
        }

        static void FeedLocation()
        {
            var newLocation = SqlGeography.Point(47.6097, -122.3331, 4326);
            while (true)
            {
                Console.WriteLine("Feeding new location, {0}", newLocation);
                myLocation.OnNext(newLocation);
                newLocation = getLocation(newLocation, 10);
                Thread.Sleep(1000);
            }
        }

        static void DisplayLocation()
        {
            var disposable = myLocation.Subscribe(new LocationObserver("Mohamed's Location"));
        }

        public static SqlGeography getLocation(SqlGeography g0, int radius)
        {
            double x0 = (double)(g0.Long);
            double y0 = (double)(g0.Lat);
            Random random = new Random();
            double radiusInDegrees = radius / 111000f;
            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(y0);
                        
            // Set the adjusted location
            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;
            return SqlGeography.Point(foundLatitude, foundLongitude, 4326);
        }
    }

}
