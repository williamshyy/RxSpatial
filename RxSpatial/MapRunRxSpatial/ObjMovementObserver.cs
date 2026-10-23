// Copyright (c) The RxSpatial Authors and Contributors. All rights reserved.
// Licensed under the MIT License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maps.MapControl.WPF;
using Microsoft.SqlServer.Types;
using System.Data.SqlTypes;
using System.Windows.Media;

using RxSpatial;

namespace MapRunRxSpatial
{
    class ObjMovementObserver:IObserver<SqlGeography>
    {

        private IDisposable unsubscriber;
        private ObjMovementHandler handler;
        public ObjMovementObserver(ObjMovementHandler handler)
        {
             this.handler=handler;
        }
        public virtual void OnNext(SqlGeography loc)
        {
            handler.move(loc);
        }

        public virtual void OnCompleted()
        {
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {

        }


        public virtual void Subscribe(RxGeography provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
            handler.del();

        }
    }
}
