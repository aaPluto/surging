﻿using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Surging.Core.CPlatform.Transport.Implementation
{
    public class RpcContext
    {
        private ConcurrentDictionary<String, Object> contextParameters;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ConcurrentDictionary<String, Object> GetContextParameters()
        {
            return contextParameters;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAttachment(string key, object value)
        {
            contextParameters.AddOrUpdate(key, value, (k, v) => value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetAttachment(string key)
        {
            contextParameters.TryGetValue(key, out object result);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetContextParameters(ConcurrentDictionary<String, Object> contextParameters)
        {
            this.contextParameters = contextParameters;
        }

        private static AsyncLocal<RpcContext> rpcContextAsyncLocal = new AsyncLocal<RpcContext>();

        public static RpcContext GetContext()
        {
            var context = rpcContextAsyncLocal.Value;
            if (context == null)
            {
                context = new RpcContext();
                context.SetContextParameters(new ConcurrentDictionary<string, object>());
                rpcContextAsyncLocal.Value = context;
            }
            return rpcContextAsyncLocal.Value;
        }

        public static void RemoveContext()
        {
            rpcContextAsyncLocal.Value = null;
        }

        private RpcContext()
        {
        }
    }
}