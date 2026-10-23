// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;

namespace MapRunRxSpatial
{
    class Parameters
    {
        //public static   string dataPath=@"C:/trafficData/";
        public static string dataPath = @"../../trafficData/";
        public static   string trafficFilename="trafficData.txt"; 
        public static string polygonFilename = "polygons.txt";
        public static string subscriptionFilename = "subscribeSettings.txt";
        public static string loadTestParamsFilename = "loadTestParams.txt";
        public static int objectsSpeedOnScreenInMS = 100;
    }


}
