// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSpatial.Util
{
    class AvgCalc
    {
        int cnter = 0;
        double avg = 0;
        public double recordLatency(double latency)
        {
            avg = avg + (latency - avg) / (cnter + 1);
            cnter++;
            return avg;
        }
        public double Avg { get { return avg; } }
    }
}
