// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;

namespace RxSpatial
{
    class RxRxGeoIntersectObserver : RxRxGeoObserver<Boolean>, IObserver<SqlGeography>
    {
     

        public RxRxGeoIntersectObserver(RxGeography G, IObserver<Boolean> o)
            : base(G,o)
        {
           
        }

        public virtual void OnNext(SqlGeography g)
        {
            if (_localG.loc.STIntersects(g).IsTrue)
                _observer.OnNext(true);
            else
                _observer.OnNext(false);
        }
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception e)
        {
        }
    }
}
