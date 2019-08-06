using System;
using System.Collections.Generic;
using System.Linq;

namespace MhLabs.Metrics
{
    public class MetricClient
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
        public MetricClient(string host, string prefix = Constants.DefaultPrefix, Action<string> output = null, List<string> defaultTags = null) 
        {
            _host = host;
            _prefix = prefix;
            _output = output ?? Console.WriteLine;

            var tags = defaultTags ?? new List<string>();
            tags.Add($"domain:{_host}");

            _defaultTags = ToTagsFormat(tags.Distinct().ToList());
        }

        public void Gauge(string name, int point, List<string> tags = null)
        {
            Print(MetricType.gauge, name, point, tags);
        }

        public void Timing(string name, string type, int point, List<string> tags = null)
        {
            Print(MetricType.rate, name, point, tags);
        }

        public void Increment(string name, int point = 1, List<string> tags = null)
        {
            Print(MetricType.count, name, point, tags);
        }

        public void Print(MetricType metricType, string name, int point = 1, List<string> tags = null)
        {
            var tagFormat = ToTagsFormat(tags);

            if (string.IsNullOrWhiteSpace(tagFormat)) 
            {
                Console.WriteLine($"{_prefix}: {_host} {metricType.ToString()} {name} {point} {_defaultTags}");
            }
            else 
            {
                Console.WriteLine($"{_prefix}: {_host} {metricType.ToString()} {name} {point} {_defaultTags} {tagFormat}");
            }
        }

        private static string ToTagsFormat(List<string> tags)
        {
            return tags == null ? string.Empty : string.Join(" ", tags);
        }
    }

    public enum MetricType
    {
        gauge,
        count,
        rate
    }

}