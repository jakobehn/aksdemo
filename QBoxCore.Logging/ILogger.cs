using System;
using System.Collections.Generic;

namespace QBox.Logging
{
    public interface ILogger
    {
        void PageView(string page);
        void Event(string eventName, IDictionary<string, string> properties = null);
        void Metric(string name, double value);
        void Exception(Exception ex);
    }
}