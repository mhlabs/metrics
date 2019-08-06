namespace MhLabs.Metrics
{
    public interface IMetricClient
    {
        void Gauge(string name, int point, params string[] tags);
        void Timing(string name, string type, int point, params string[] tags);
        void Increment(string name, int point = 1, params string[] tags);
    }
}