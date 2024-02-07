using Microsoft.Extensions.DependencyInjection;
using UserRegistration.DAL.Repositories;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Extensions
{
    public static class ServiceCollectionDalExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IUserDataRepository, UserDataRepository>();
        }
    }
}
