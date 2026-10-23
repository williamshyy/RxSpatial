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
    public class RxSpatialOverLoadException:System.Exception
    {
        public RxSpatialOverLoadException() : base() { }
        public RxSpatialOverLoadException(string message) : base(message) { }
        public RxSpatialOverLoadException(string message, System.Exception inner)
            : base(message, inner) { }

        protected RxSpatialOverLoadException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
