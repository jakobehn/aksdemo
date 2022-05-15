using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;
using QBoxCore.Common;

namespace QBox.Logging
{
    public class Logger : ILogger
    {
        private readonly QBoxAppInsightsSettings settings;
        private static TelemetryClient telemetryClient;


        public Logger(QBoxAppInsightsSettings settings)
        {
            this.settings = settings;
            if( telemetryClient == null)
            {
                telemetryClient = new TelemetryClient()
                {
                    InstrumentationKey = settings.InstrumentationKey,
                };
                telemetryClient.Context.Component.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public void PageView(string page)
        {
            telemetryClient.TrackPageView(page);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + "Page: " + page);
        }
        public void Event(string eventName, IDictionary<string, string> properties = null)
        {
            if (properties == null)
            {

                telemetryClient.TrackEvent(eventName);
            }
            else
            {
                telemetryClient.TrackEvent(eventName, properties, null);
            }
            string log = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + eventName + Environment.NewLine;
            if ( Directory.Exists("/var/lib/storage"))
            { 
                string path = "/var/lib/storage/log.txt";


                File.AppendAllText(path, log);
            }
            Console.WriteLine(log);
        }

        public void Metric(string name, double value)
        {
            telemetryClient.TrackMetric(name, value);
        }

        public void Exception(Exception ex)
        {
            telemetryClient.TrackException(ex);
        }
    }
}
