using ElegantGlamour.Core;
using ElegantGlamour.Core.Services;
using ElegantGlamour.Data;
using ElegantGlamour.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElegantGlamour.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPrestationService, PrestationService>();

            return services;
        }
    }
}