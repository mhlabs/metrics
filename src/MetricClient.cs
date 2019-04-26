using System;

namespace MhLabs.Metrics
{
    public class MetricClient
    {
        public static string Host = "mathem.my-service";
        public static string Prefix = "mathem.metric";
        public static Action<string> Renderer = Console.WriteLine;

        public MetricClient() {}
        public MetricClient(string host, string prefix, Action<string> renderer) 
        {
            Host = host;
            Prefix = prefix;
            Renderer = renderer;
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