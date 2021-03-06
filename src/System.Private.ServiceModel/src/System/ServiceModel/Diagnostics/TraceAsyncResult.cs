// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime;
using System.Diagnostics;

namespace System.ServiceModel.Diagnostics
{
    internal abstract class TraceAsyncResult : AsyncResult
    {
        private static Action<AsyncCallback, IAsyncResult> s_waitResultCallback = new Action<AsyncCallback, IAsyncResult>(DoCallback);

        protected TraceAsyncResult(AsyncCallback callback, object state) :
            base(callback, state)
        {
            if (TraceUtility.MessageFlowTracingOnly)
            {
                base.VirtualCallback = s_waitResultCallback;
            }
            else if (DiagnosticUtility.ShouldUseActivity)
            {
                this.CallbackActivity = ServiceModelActivity.Current;
                if (this.CallbackActivity != null)
                {
                    base.VirtualCallback = s_waitResultCallback;
                }
            }
        }

        public ServiceModelActivity CallbackActivity
        {
            get;
            private set;
        }

        private static void DoCallback(AsyncCallback callback, IAsyncResult result)
        {
            if (result is TraceAsyncResult)
            {
                TraceAsyncResult thisPtr = result as TraceAsyncResult;
                Fx.Assert(thisPtr.CallbackActivity != null, "this shouldn't be hooked up if we don't have a CallbackActivity");

                if (TraceUtility.MessageFlowTracingOnly)
                {
                    thisPtr.CallbackActivity = null;
                }

                using (ServiceModelActivity.BoundOperation(thisPtr.CallbackActivity))
                {
                    callback(result);
                }
            }
        }
    }
}
