using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flavehub.Repository.ServiceImplementation;
using flavehub.Repository.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace flavehub.Installers
{
    public class SocialAccountInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("GitHub", client => {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
            });

            services.AddSingleton<IGithubService, GithubService>();
        }
    }
}
