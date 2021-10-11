using DowntownCarClub.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DowntownCarClub.Web.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<EmailService>();
            return services;
        }
    }
}
