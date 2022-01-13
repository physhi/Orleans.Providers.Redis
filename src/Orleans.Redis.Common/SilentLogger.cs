namespace Orleans.Redis.Common
{
    using Serilog;

    public class SilentLogger
    {
        public static readonly ILogger Logger = new LoggerConfiguration().CreateLogger();
    }
}
