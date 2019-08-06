namespace MhLabs.Metrics
{
    public class NullClient : IMetricClient
    {
        public void Gauge(string name, int point, params string[] tags)
        {}

        public void Timing(string name, string type, int point, params string[] tags)
        {}

        public void Increment(string name, int point = 1, params string[] tags)
        {}

        public static IMetricClient Create() => new NullClient();
    }
}