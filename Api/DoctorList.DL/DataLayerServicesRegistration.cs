using DoctorList.DL.Entities;
using DoctorList.DL.Interfaces;
using DoctorList.DL.Repositories;
using DoctorList.DL.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL
{
    public static class DataLayerServicesRegistration
    {
        public static IServiceCollection ConfigureDataLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration
            services.Configure<DoctorFileDataSettings>(configuration.GetSection("DoctorFileData"));

            //Repositories
            services.AddSingleton<IContactUsRepository, ContactUsRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            return services;
        }
    }
}
