using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flavehub.Cache;
using flavehub.Data;
using flavehub.Health;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace flavehub.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new RedisCacheSettings();

            if (!redisSettings.Enabled)
            {
                services.AddHealthChecks().Services.AddDbContext<DataContext>();
            }
            else
            {
                services.AddHealthChecks()
                .AddCheck<RedisHealthCheck>("Redis");
            }

        }
        }
}
