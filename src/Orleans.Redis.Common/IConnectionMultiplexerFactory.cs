namespace Orleans.Redis.Common
{
    using StackExchange.Redis;

    using System.Threading.Tasks;

    public interface IConnectionMultiplexerFactory
    {
        /// <summary>
        /// Call to create a new (or yield a previously created) <see cref="IConnectionMultiplexer"/>.
        /// Callers should not manually dispose the connections and instead call <see cref="ReleaseAsync(IConnectionMultiplexer)"/>.
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        Task<IConnectionMultiplexer> CreateAsync(string configuration);
    }
}
