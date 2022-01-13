namespace Orleans.Redis.Common
{
    using System.Collections.Generic;

    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    public static class SerilogExtensions
    {
        public static ILogger ForContext<T>(this ILogger logger, IDictionary<string, object> properties, bool destructureObjects = false)
        {
            return logger.ForContext<T>().ForContext(properties, destructureObjects);
        }

        public static ILogger ForContext(this ILogger logger, IDictionary<string, object> properties, bool destructureObjects = false)
        {
            return properties == null || properties.Count == 0
                ? logger
                : logger.ForContext(new DictionaryEventEnricher(properties, destructureObjects));
        }

        private class DictionaryEventEnricher : ILogEventEnricher
        {
            private readonly IDictionary<string, object> _properties;
            private readonly bool _destructureObjects;

            public DictionaryEventEnricher(IDictionary<string, object> properties, bool destructureObjects)
            {
                _properties = properties;
                _destructureObjects = destructureObjects;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                foreach (var kvp in _properties)
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(kvp.Key, kvp.Value, _destructureObjects));
                }
            }
        }
    }
}
