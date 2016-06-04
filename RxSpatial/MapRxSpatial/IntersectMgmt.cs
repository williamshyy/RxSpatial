using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace MapRxSpatial
{
    class IntersectMgmt
    {
        public static List<SqlGeography> polygonList = new List<SqlGeography>();
        public static void initPolygon()
        {
            
            TextReader tr = new StreamReader(Parameters.dataPath + Parameters.polygonFilename);
            string line = null;
            for (line = tr.ReadLine(); line != null; line = tr.ReadLine())
            {
                addPolygon(line);
            }
        }
        public static void addPolygon(string line){
            SqlGeography polygon = SqlGeography.STPolyFromText(new SqlChars(new SqlString(line)), 4326);
           /* SqlGeography polygon = SqlGeography.STPolyFromText(
                   new SqlChars(
                   new SqlString("POLYGON ((-122.2 47.5, -122.3 47.5,"
                       + "-122.3 47.6, -122.2 47.6, -122.2 47.5))")),
                       4326);*/
            polygonList.Add(polygon);
        }
    }
}
