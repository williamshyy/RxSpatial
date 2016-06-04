using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace MapRxSpatial
{
    class Parameters
    {
        public static   string dataPath=@"C:/trafficData/";
        public static   string trafficFilename="trafficData.txt";
        public static string polygonFilename = "polygons.txt";
        public static int objectsSpeedOnScreenInMS = 200;
    }


}
