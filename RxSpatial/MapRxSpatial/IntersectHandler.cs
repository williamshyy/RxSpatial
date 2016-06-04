using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
namespace MapRxSpatial
{
    class IntersectHandler:MovingObjHandler
    {

       // int id;
        public IntersectHandler( Map baseMap,int id)
            : base(id,baseMap)
        {
          
        }
        public void proc(SqlGeography newLoc, Boolean b)
        {
            if (b)
                move(newLoc,id,Brushes.Red);
            else
                move(newLoc, id, Brushes.Blue);

        }



    }
}
