using Microsoft.Extensions.DependencyInjection;
using UserRegistration.BLL.Services;
using UserRegistration.BLL.Services.Interfaces;

namespace UserRegistration.BLL.Extensions
{
    public static class ServiceCollectionBllExtensions
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
