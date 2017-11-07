using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using Common.Constants;
using DataLayer.DbContext;
using DataLayer.Entities.Identity;
using DataLayer.Repositories.Abstractions;
using DataLayer.Repositories.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TrackYourLife.API.Infrastructure
{
    public static class DiContainer
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // App Services
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConfigurationConstants.DbConnection), ServiceLifetime.Scoped);

            services.AddIdentity<AppUser, IdentityRole>(conf =>
            {
                conf.Password.RequiredLength = 6;
                conf.Password.RequireDigit = false;
                conf.Password.RequireLowercase = false;
                conf.Password.RequireUppercase = false;
                conf.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            // Data Layer

            services.AddScoped<IDonorOrganRequestRepository, DonorOrganRequestRepository>();
            services.AddTransient<IOrganInfoRepository, OrganInfoRepository>();
            services.AddTransient<IClinicsRepository, ClinicsRepository>();
            services.AddTransient<IMedicalExamsRepository, MedicalExamsRepository>();
            services.AddTransient<IOrganDeliveryRepository, OrganDeliveryRepository>();
            services.AddTransient<ITransplantOrgansRepository, TransplantOrgansRepository>();
            services.AddTransient<IOrganDeliverySnapshotsRepository, OrganDeliverySnapshotsRepository>();


            // Business Layer
            services.AddTransient<IDonorOrganRequestService, DonorOrganRequestService>();
            services.AddTransient<IOrganInfoService, OrganInfoService>();
            services.AddTransient<ITransplantOrgansService, TransplantOrgansService>();
            services.AddTransient<IClinicsService, ClinicsService>();
            services.AddTransient<IPatientOrganRequestService, PatientOrganRequestService>();
            services.AddTransient<IPatientQueueService, PatientQueueService>();
            services.AddTransient<IOrganTransportService, OrganTransportService>();
        }
    }
}
