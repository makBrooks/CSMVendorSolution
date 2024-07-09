using Admin.Application.Interfaces;
using Admin.Infastructure.Redis.Base;
using Admin.Infra.Data.Factory;
using Admin.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Infra.Extension
{
    public static class InfraService
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            IConnectionFactory connectionFactory = new ConnectionFactory(configuration.GetConnectionString("DBconnection")!);
            services.AddSingleton<IConnectionFactory>(connectionFactory);

            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IRedisCache, RedisCache>();
        }
    }
}
