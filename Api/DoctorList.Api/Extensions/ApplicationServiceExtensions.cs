using DoctorList.DL;
using DoctorList.BL;

namespace DoctorList.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register IMemoryCache
            services.AddMemoryCache();

            services.ConfigureDataLayerServices(configuration);
            services.ConfigureBusinessLayerServices(configuration);

            return services;
        }
    }
}
