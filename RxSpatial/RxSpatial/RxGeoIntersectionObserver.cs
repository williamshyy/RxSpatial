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
    class RxGeoIntersectionObserver : RxGeoObserverContainer<SqlGeography>, IObserver<SqlGeography>
    {
        public object G
        { get { return this._localG; } }

        public RxGeoIntersectionObserver(SqlGeography G, IObserver<SqlGeography> o)
            : base(o)
        {
            _localG = G;
        }

        public virtual void OnNext(SqlGeography g)
        {
           _observer.OnNext(_localG.STIntersection(g));
        }
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception e)
        {
        }

        private SqlGeography _localG;
    }
}
