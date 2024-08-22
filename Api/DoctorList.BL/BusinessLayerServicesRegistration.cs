using DoctorList.BL.Interface;
using DoctorList.BL.Service;
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

namespace DoctorList.BL
{
    public static class BusinessLayerServicesRegistration
    {
        public static IServiceCollection ConfigureBusinessLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IDoctorService, DoctorService>();
            return services;
        }
    }
}
