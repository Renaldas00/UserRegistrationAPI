using Microsoft.Extensions.DependencyInjection;
using UserRegistration.BLL.Interfaces;
using UserRegistration.BLL.Services;

namespace UserRegistration.BLL.Extensions
{
    public static class ServiceCollectionBllExtensions
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IUserManagerService, UserManagerService>();
            services.AddTransient<IJwtService, JwtService>();
        }
    }
}
