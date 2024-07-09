using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Admin.Infastructure.Redis.ConnectionFactory
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json")
                               .AddEnvironmentVariables().Build();

            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisCacheUrl"));
            });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
