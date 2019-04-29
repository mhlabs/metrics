using System;

namespace MhLabs.Metrics
{
    public class MetricClient
    {
        public static string Host;
        public static string Prefix;
        public static Action<string> Renderer;

        public MetricClient(string host = null, string prefix = null, Action<string> renderer = null) 
        {
            Host = host ?? "mathem.my-service";
            Prefix = prefix ?? "mathem.metric";
            Renderer = renderer ?? Console.WriteLine;;
        }

        public void Gauge(string name, string type, int point, string[] tags = null)
        {
            Print(MetricType.gauge, name, type, point, tags);
        }

        public void Timing(string name, string type, int point, string[] tags = null)
        {
            Print(MetricType.rate, name, type, point, tags);
        }

        public void Increment(string name, string type, int point = 1, string[] tags = null)
        {
            Print(MetricType.count, name, type, point, tags);
        }

        public void Print(MetricType metricType, string name, string type, int point = 1, string[] tags = null)
        {
            if (tags == null) 
            {
                Console.WriteLine($"{Prefix}: {Host} {metricType.ToString()} {name} {point}");
            }
            else 
            {
                Console.WriteLine($"{Prefix}: {Host} {metricType.ToString()} {name} {point} {string.Join(" ", tags)}");
            }
        }
    }

    public enum MetricType
    {
        gauge,
        count,
        rate
    }

}