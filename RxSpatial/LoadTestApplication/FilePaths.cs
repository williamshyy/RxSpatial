using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadTestApplication
{
    class FilePaths
    {
        //public static   string dataPath=@"C:/trafficData/";
        public static string dataPath = @"../../";
        public static   string trafficFilename="trafficData.txt"; 
        public static string polygonFilename = "polygons.txt";
        public static string subscriptionFilename = "subscribeSettings.txt";
        public static string loadTestParamsFilename = "loadTestParams.txt";

        public static string batchLoadTestParamsXml = "BatchLoadParams.xml";
        public static string batchLoadTestParamsFilename = "BatchLoadParams.txt";
        public static string batchLoadTestOutputFilenameVnum = "MovingObjNum";
        public static string batchLoadTestOutputFilenameInterval = "Latency";
        public static string batchLoadTestOutputMaxLatencyFilenameInterval = "MaxLatency";
    }


}
