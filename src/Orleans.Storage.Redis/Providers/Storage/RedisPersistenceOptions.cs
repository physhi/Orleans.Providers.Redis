﻿namespace Orleans.Configuration
{
    using Orleans.Runtime;

    using System;

    public class RedisGrainStorageOptions : RedisOptions
    {
        /// <summary>
        /// Stage of silo lifecycle where storage should be initialized.  Storage must be initialized prior to use.
        /// </summary>
        public int InitStage { get; set; } = DEFAULT_INIT_STAGE;
        public const int DEFAULT_INIT_STAGE = ServiceLifecycleStage.ApplicationServices;

        public PersistenceLifetime PersistenceLifetime { get; set; } = DEFAULT_PERSISTENCE_LIFETIME;
        public const PersistenceLifetime DEFAULT_PERSISTENCE_LIFETIME = PersistenceLifetime.ServiceLifetime;

        public bool ThrowExceptionOnInconsistentETag { get; set; } = true;

        /// <summary>
        /// Rethrow exceptions that occur during Read/Write/Clear operations. The default behavior is to not
        /// rethrow.
        /// </summary>
        public bool RethrowExceptions { get; set; } = false;
    }

    public class RedisGrainStorageOptionsValidator : IConfigurationValidator
    {
        private readonly RedisGrainStorageOptions options;
        private readonly string name;

        public RedisGrainStorageOptionsValidator(RedisGrainStorageOptions options, string name)
        {
            this.options = options;
            this.name = name;
        }

        public void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                throw new OrleansConfigurationException(
                    $"{nameof(RedisGrainStorageOptions)} on stream provider {this.name} is invalid. {nameof(RedisGrainStorageOptions.ConnectionString)} is invalid");
            }
        }
    }
}
