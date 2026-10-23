// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace MapRunRxSpatial
{
    abstract class GeoObserver
    {
        protected ObjMovementHandler _objMovementHandler;
        public GeoObserver(ObjMovementHandler objMovementHandler)
        {
            _objMovementHandler = objMovementHandler;
        }
    }
}
