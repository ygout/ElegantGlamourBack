using ElegantGlamour.Core.Models.Entity.Auth;
using ElegantGlamour.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ElegantGlamour.Api.Extensions
{
    public static class IdentityServiceExtensionsd
    {
       public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, Role>();
            builder.AddEntityFrameworkStores<ElegantGlamourDbContext>();
            builder.AddDefaultTokenProviders();
            return services;
        }
    }
}