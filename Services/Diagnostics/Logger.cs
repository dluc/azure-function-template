// Copyright (c) Microsoft. All rights reserved.

using System;
using Microsoft.Azure.WebJobs.Host;

namespace Microsoft.Azure.IoTSolutions.Services.Diagnostics
{
    public interface ILogger
    {
        void SetWriter(TraceWriter log);
        void Trace(TraceEvent traceEvent);
        void Verbose(string message, string source = null);
        void Info(string message, string source = null);
        void Warning(string message, string source = null);
        void Error(string message, Exception ex = null, string source = null);
        void Flush();
    }

    public class Logger : ILogger
    {
        private TraceWriter logger;

        public void SetWriter(TraceWriter writer)
        {
            this.logger = writer;
        }

        public void Trace(TraceEvent traceEvent)
        {
            this.logger?.Trace(traceEvent);
        }

        public void Verbose(string message, string source = null)
        {
            this.logger?.Verbose(message, source);
        }

        public void Info(string message, string source = null)
        {
            this.logger?.Info(message, source);
        }

        public void Warning(string message, string source = null)
        {
            this.logger?.Warning(message, source);
        }

        public void Error(string message, Exception ex = null, string source = null)
        {
            this.logger?.Error(message, ex, source);
        }

        public void Flush()
        {
            this.logger?.Flush();
        }
    }
}
