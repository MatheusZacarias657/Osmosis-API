using Service;
using Repository;
using Microsoft.AspNetCore.Authentication;
using Osmosis.API.Authentication;

namespace Osmosis.API
{
    public static class Inicialize
    {
        public static IServiceCollection InicializeDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Repository
            services.InicializeRepository(configuration);

            //Add Services
            services.InicializeService(configuration);

            //Add OAuth
            services.AddHttpContextAccessor();
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, APIAuthentication>("BasicAuthentication", null);

            return services;
        }
    }
}
