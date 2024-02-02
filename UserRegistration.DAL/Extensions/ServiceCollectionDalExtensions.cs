using Microsoft.Extensions.DependencyInjection;
using UserRegistration.DAL.Interfaces;
using UserRegistration.DAL.Repositories;

namespace UserRegistration.DAL.Extensions
{
    public static class ServiceCollectionDalExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonalDataListRepository, PersonalDataListRepository>();
            services.AddTransient<ILocationListRepository, LocationListRepository>();
            services.AddTransient<IPhotoListRepository, PhotoListRepository>();
        }
    }
}
