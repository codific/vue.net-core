using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
