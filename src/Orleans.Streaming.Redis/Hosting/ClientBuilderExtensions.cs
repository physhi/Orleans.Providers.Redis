namespace Orleans.Hosting
{
    using Orleans.Streaming;

    using System;

    public static class ClientBuilderExtensions
    {
        /// <summary>
        /// Configure cluster client to use redis queue persistent streams. Returns ClusterClientRedisStreamConfigurator for further configuration
        /// </summary>
        public static ClusterClientRedisStreamConfigurator AddRedisStreams(this IClientBuilder builder, string name)
        {
            return new ClusterClientRedisStreamConfigurator(name, builder);
        }

        /// <summary>
        /// Configure cluster client to use redis queue persistent streams. 
        /// </summary>
        public static IClientBuilder AddRedisStreams(this IClientBuilder builder, string name, Action<ClusterClientRedisStreamConfigurator> configure)
        {
            configure?.Invoke(builder.AddRedisStreams(name));
            return builder;
        }
    }
}
