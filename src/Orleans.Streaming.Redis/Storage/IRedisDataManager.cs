namespace Orleans.Streaming.Redis.Storage
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using StackExchange.Redis;

    internal interface IRedisDataManager
    {
        string QueueName { get; }

        Task AddQueueMessage(byte[] payload);
        Task DeleteQueueMessage(RedisValue value);
        Task<IEnumerable<RedisValue>> GetQueueMessagesAsync(int count);
        Task InitAsync(CancellationToken ct = default);
        Task SubscribeAsync(CancellationToken ct = default);
        Task UnsubscribeAsync(CancellationToken ct = default);
    }
}