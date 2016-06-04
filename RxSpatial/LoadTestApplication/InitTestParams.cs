using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace LoadTestApplication
{
     /*class Subscription{
                private string _name;
                private string _clientId;
                private string _observerType;
                public Subscription(string name,string clientId,string observerType){
                    _name=name;_clientId=clientId;_observerType=observerType;
                }
                public string Name{ get { return this._name; } }
                public string ClientId{ get { return this._clientId; } }
                public string ObserverType{ get { return this._observerType; } }
            }*/
    class SubscribeParams
    {
        //PolygonList to test intersection observer only
        public static Dictionary<string, SqlGeography> polygonList = new Dictionary<string, SqlGeography>();
 
      
        //subscriptionList to subscribe observers, in different clientIds oriented
        //public static Dictionary<string, List<Subscription>> subscriptionList = new Dictionary<string,List<Subscription>>();

        public static void init()
        {
            initPolygon();
           // initSubscription();
        }

        private static void initPolygon()
        {

          /*  TextReader tr = new StreamReader(FilePaths.dataPath + FilePaths.polygonFilename);
            string line = null;
            for (line = tr.ReadLine(); line != null; line = tr.ReadLine())
            {
                string[] strs = line.Split(' ');
                int idx = line.IndexOf(" ");
                addPolygon(strs[0], line.Substring(idx));

               // Console.WriteLine(line);
            }*/
            SqlGeography[,] subGrid = initPolygonGrid(100, 100, 46, -124, 49, -120);
            procGridMatrix(subGrid, 100);

        }
        public static void procGridMatrix(SqlGeography[,] subGrid ,int n) {
           
        int upper=n-1, lower=0;
        int i=0, j=0;

        for(int k=1; k<=n*n; k++){
            polygonList[((n * n) - k).ToString()] = subGrid[i, j];

            if(i==lower&&j<upper) j++;
            else if(j==upper&&i<upper) i++;
            else if(i==upper&&j>lower) j--;
            else if(j==lower&&n>lower) i--;

            if(i==lower&&j==lower){
                i++;
                j++;
                lower++;
                upper--;
            }
        }
    }
        public static SqlGeography[,] initPolygonGrid(int latNum, int longNum, double lat1, double long1, double lat2, double long2)
        {

            double latStep = (lat2 - lat1) / latNum;
            double longStep = (long2 - long1) / latNum;
            //  grid = new HashSet<SqlGeography>[latNum, longNum];
            SqlGeography[,] subGrid = new SqlGeography[latNum, longNum];
            for (int i = 0; i < latNum; i++)
                for (int j = 0; j < longNum; j++)
                {
                    string polygon = String.Format("POLYGON(({0} {1},{2} {3},{4} {5},{6} {7},{8} {9}))",
                    ( long1 + (j + 1) *  longStep), ( lat1 + i *  latStep),
                    ( long1 + (j + 1) *  longStep), ( lat1 + (i + 1) *  latStep),
                    ( long1 + j *  longStep), ( lat1 + (i + 1) *  latStep),
                    ( long1 + j *  longStep), ( lat1 + i *  latStep),
                    ( long1 + (j + 1) *  longStep), ( lat1 + i *  latStep));
                    subGrid[i, j] = SqlGeography.STPolyFromText(new SqlChars(new SqlString(polygon)), 4326);
                }
            return subGrid;

        }
        private static void addPolygon(string name, string line)
        {
            SqlGeography polygon = SqlGeography.STPolyFromText(new SqlChars(new SqlString(line)), 4326);
            polygonList.Add(name, polygon);
        }
      /*  private static void initSubscription()
        {
            TextReader tr = new StreamReader(FilePaths.dataPath + FilePaths.subscriptionFilename);
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] strs = line.Split(' ');
                addSubscripton(strs[0], strs[1], strs[2]);
                line = tr.ReadLine();
            }
        }

        private static void addSubscripton(string name, string clientId, string observerType)
        {
            if(!subscriptionList.ContainsKey(clientId)){
                subscriptionList.Add(clientId,new List<Subscription>());
            }
            Subscription subscription = new Subscription(name,clientId,observerType);
            subscriptionList[clientId].Add(subscription);
        }*/
    }
    class LoadParams
    {
        public static int observerNumber=-1;
        public static int vehicleNumber=-1;
        public static int updateTimeInterval = -1;
        public static void init()
        {
            TextReader tr = new StreamReader(FilePaths.dataPath + FilePaths.loadTestParamsFilename);
            string line = tr.ReadLine();
            while (line != null)
            {
                string[] strs = line.Split(' ');
                if (strs[0].Equals("ObserverNumber:")) observerNumber = Convert.ToInt32(strs[1]);
                else if (strs[0].Equals("VehicleNumber:")) vehicleNumber = Convert.ToInt32(strs[1]);
                else if (strs[0].Equals("UpdateTimeInterval:")) updateTimeInterval = Convert.ToInt32(strs[1]);
                line = tr.ReadLine();
            }
        }
    }

    class BatchLoadParams
    {
        public class TestCase
        {
            public  int observerNumber = -1;
            public  int vehicleNumber = -1;
            public  int updateTimeInterval = -1;
            public string subscribeType = null;
            public TestCase(int observerNumber, int vehicleNumber, int updateTimeInterval, string subscribeType)
            {
                this.observerNumber = observerNumber;
                this.vehicleNumber = vehicleNumber;
                this.updateTimeInterval = updateTimeInterval;
                this.subscribeType = subscribeType;
            }
            public Boolean output;
            public TimeSpan ts;
            public double latency;
            public double maxLatency;
            public AvgCalc latencyRecorder = new AvgCalc();

        }

        public static List<List<TestCase>> batchLoadList = new List<List<TestCase>>();


        public static void init()
        {

            /*for (int updateTimeInterval = 100; updateTimeInterval <= 1000; updateTimeInterval = updateTimeInterval + 100)
            {
                for (int observerNumber = 2; observerNumber <= 50; observerNumber = observerNumber + 2)
                {
                    batchLoadList.Add(new TestCase(observerNumber, 10, updateTimeInterval));
                }
            }*/
           /* int updateTimeInterval = 400;
            for (int observerNumber = 1; observerNumber <= 50; observerNumber = observerNumber + 1)
            {
                List<TestCase> tmploadList = new List<TestCase>();

                for (int vNum = 1; vNum <= 50; vNum = vNum + 1)
                {
                    tmploadList.Add(new TestCase(observerNumber, vNum, updateTimeInterval));
                }
            }*/
            /*

            for (int updateTimeInterval = 500; updateTimeInterval <= 800; updateTimeInterval = updateTimeInterval + 100)
            {
                List<TestCase> tmploadList = new List<TestCase>();
                for (int observerNumber = 1; observerNumber <= 50; observerNumber = observerNumber + 1)
                {
                    tmploadList.Add(new TestCase(observerNumber, 2, updateTimeInterval));
                }
                batchLoadList.Add(tmploadList);
            }*/
          /*  TextReader tr = new StreamReader(FilePaths.dataPath + FilePaths.batchLoadTestParamsFilename);
            string line = tr.ReadLine();
             line = tr.ReadLine();
             
            while (line != null)
            {
                Console.WriteLine(line);
                string[] strs = line.Split(' ');
                if (strs.Length != 3)
                    continue;
                batchLoadList.Add(new TestCase(Convert.ToInt32(strs[0]), 
                    Convert.ToInt32(strs[1]), Convert.ToInt32(strs[2])));
                line = tr.ReadLine();
            }*/

        }
    }
}
