namespace MhLabs.Metrics
{
    public interface IMetricClient
    {
        /// <summary>
        /// A gauge is a metric that represents a single numerical value that can arbitrarily go up and down.
        /// Gauges are typically used for measured values like temperatures or current memory usage, but also "counts" that can go up and down, like the number of concurrent requests.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="point"></param>
        /// <param name="tags"></param>
        void Gauge(string name, int point, params string[] tags);

        /// <summary>
        /// Rates represent the derivative of a metric, it’s the value variation of a metric on a defined time interval. E.g execution duration of a single function.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="point"></param>
        /// <param name="tags"></param>
        void Timing(string name, string type, int point, params string[] tags);

        /// <summary>
        /// Counters are used to count things. Incremented by default by 1. E.g increment every time a user performs a search.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="point"></param>
        /// <param name="tags"></param>
        void Increment(string name, int point = 1, params string[] tags);
    }
}