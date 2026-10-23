// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRunRxSpatial
{
    enum STATE
    {
        INTERSCT, DIST, RXRXDIST, RXRXINTERSECTION, RXRXDISTCONN
    };
    class ParamMgr
    {
        public static int MovementIntervalInMS = 1000;
        public static int VehicleNumber = 1;
        public static double ObsvDist = 0;
        public static STATE subscribeState = STATE.INTERSCT;
        public void updateParam(){

        }
    }
}
