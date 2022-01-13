namespace Orleans.Hosting
{
    using Orleans.Streaming;

    using System;

    public static class SiloBuilderExtensions
    {
        /// <summary>
        /// Configure silo to use redis queue persistent streams. 
        /// </summary>
        public static ISiloHostBuilder AddRedisStreams(this ISiloHostBuilder builder, string name, Action<SiloRedisStreamConfigurator> configure)
        {
            var configurator = new SiloRedisStreamConfigurator(
                name,
                x => builder.ConfigureServices(x),
                x => builder.ConfigureApplicationParts(x));

            configure?.Invoke(configurator);
            return builder;
        }
    }
}
