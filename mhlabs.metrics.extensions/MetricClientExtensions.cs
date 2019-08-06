using System;
using System.Collections.Generic;
using MhLabs.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace mhlabs.metrics.extensions
{
    public static class MetricClientExtensions
    {
        /// <param name="serviceCollection"></param>
        /// <param name="host">Mandatory. Assign the metric host or source, e.g 'mathem.my-service'. Will automatically be added as a default tag: 'domain:host'</param>
        /// <param name="prefix">Add your custom prefix for metrics, e.g 'mathem.metric'</param>
        /// <param name="output">Metric renderer, are using Console.WriteLine as default</param>
        /// <param name="defaultTags">List of default tags that will be included in all logs, e.g 'owner:ecom'</param>
        public static IServiceCollection AddMetricsClient(this IServiceCollection serviceCollection, 
            string host,
            string prefix = Constants.DefaultPrefix,
            Action<string> output = null, List<string> defaultTags = null)
        {
            return serviceCollection.AddSingleton(new MetricClient(host: host, prefix: prefix, output: output, defaultTags: defaultTags));
        }
    }
}
