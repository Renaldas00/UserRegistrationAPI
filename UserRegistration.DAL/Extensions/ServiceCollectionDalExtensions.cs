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
            services.AddTransient<IPhotoListRepository, PhotoListRepository>();
            services.AddTransient<ILocationListRepository, LocationListRepository>();
            services.AddTransient<IUserDataListRepository, UserDataListRepository>();
        }
    }
}
