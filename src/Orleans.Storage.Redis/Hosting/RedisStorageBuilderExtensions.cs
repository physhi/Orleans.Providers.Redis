namespace Orleans.Hosting
{
    using Orleans.Redis.Common;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Orleans.Configuration;
    using Orleans.Providers;
    using Orleans.Runtime;
    using System;
    using Orleans.Storage;

    public static class RedisStorageBuilderExtensions
    {
        /// <summary>
        /// Configure silo to use redis storage as the default grain storage.
        /// </summary>
        public static ISiloHostBuilder AddRedisGrainStorageAsDefault(this ISiloHostBuilder builder, Action<RedisGrainStorageOptions> configureOptions)
        {
            return builder.AddRedisGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use redis table storage for grain storage.
        /// </summary>
        public static ISiloHostBuilder AddRedisGrainStorage(this ISiloHostBuilder builder, string name, Action<RedisGrainStorageOptions> configureOptions)
        {
            return builder.ConfigureServices(services => services.AddRedisGrainStorage(name, ob => ob.Configure(configureOptions)));
        }

        internal static IServiceCollection AddRedisGrainStorage(this IServiceCollection services, string name, Action<OptionsBuilder<RedisGrainStorageOptions>> configureOptions = null)
        {
            configureOptions?.Invoke(services.AddOptions<RedisGrainStorageOptions>(name));

            services.TryAddSingleton(SilentLogger.Logger);
            services.TryAddSingleton(CachedConnectionMultiplexerFactory.Default);
            services.TryAddSingleton<ISerializationManager, OrleansSerializationManager>();
            _ = services.AddTransient<IConfigurationValidator>(sp => new RedisGrainStorageOptionsValidator(sp.GetService<IOptionsSnapshot<RedisGrainStorageOptions>>().Get(name), name));
            _ = services.ConfigureNamedOptionForLogging<RedisGrainStorageOptions>(name);
            services.TryAddSingleton(sp => sp.GetServiceByName<IGrainStorage>(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME));

            _ = services.AddSingletonNamedService(name, RedisGrainStorageFactory.Create);
            _ = services.AddSingletonNamedService(name, (s, n) => (ILifecycleParticipant<ISiloLifecycle>)s.GetRequiredServiceByName<IGrainStorage>(n));
            return services;
        }
    }
}
