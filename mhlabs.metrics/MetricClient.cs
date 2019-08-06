using System;
using System.Collections.Generic;
using System.Linq;

namespace MhLabs.Metrics
{
    public class MetricClient : IMetricClient
    {
        private readonly string _host;
        private readonly string _prefix;
        private readonly string _defaultTags;
        private readonly Action<string> _output;

        /// <summary>
        /// Creates an instance of MetricClient
        /// </summary>
        /// <param name="host">Mandatory. Assign the metric host or source, e.g 'mathem.my-service'. Will automatically be added as a default tag: 'domain:host'</param>
        /// <param name="prefix">Add your custom prefix for metrics, e.g 'mathem.metric'</param>
        /// <param name="output">Metric renderer, are using Console.WriteLine as default</param>
        /// <param name="defaultTags">List of default tags that will be included in all logs, e.g 'owner:ecom'</param>
        public MetricClient(string host, string prefix = Constants.DefaultPrefix, Action<string> output = null, params string[] defaultTags) 
        {
            _host = host;
            _prefix = prefix;
            _output = output ?? Console.WriteLine;

            var baseTags = new List<string> {$"domain:{_host}"};
            baseTags.AddRange(defaultTags ?? new string[0]);

            _defaultTags = ToTagsFormat(baseTags.Distinct().ToArray());
        }

        public void Gauge(string name, int point, params string[] tags)
        {
            Print(MetricType.Gauge, name, point, tags);
        }

        public void Timing(string name, string type, int point, params string[] tags)
        {
            Print(MetricType.Rate, name, point, tags);
        }

        public void Increment(string name, int point = 1, params string[] tags)
        {
            Print(MetricType.Count, name, point, tags);
        }

        public void Print(MetricType metricType, string name, int point = 1, params string[] tags)
        {
            var tagFormat = ToTagsFormat(tags);
            var type = ToMetricTypeString(metricType);

            _output(string.IsNullOrWhiteSpace(tagFormat)
                ? $"{_prefix}: {_host} {type} {name} {point} {_defaultTags}"
                : $"{_prefix}: {_host} {type} {name} {point} {_defaultTags} {tagFormat}");
        }

        private static string ToTagsFormat(string[] tags)
        {
            return tags == null ? string.Empty : string.Join(" ", tags);
        }

        private static string ToMetricTypeString(MetricType type)
        {
            switch (type)
            {
                case MetricType.Gauge: return "gauge";
                case MetricType.Count: return "count";
                case MetricType.Rate: return "rate";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum MetricType
    {
        Gauge,
        Count,
        Rate
    }

}