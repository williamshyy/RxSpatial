// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSpatial
{
    class RxRxGeoObserver<T>
    {
        public RxGeography G
        { get { return this._localG; } }
        protected RxGeography _localG;
        protected IObserver<T> _observer;
            public RxRxGeoObserver(RxGeography G, IObserver<T> o)
            {
                _localG = G;
                _observer = o;
            }
        
    }
}
