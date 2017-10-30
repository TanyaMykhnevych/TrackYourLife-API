using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using Common.Constants;
using DataLayer.DbContext;
using DataLayer.Entities.Identity;
using DataLayer.Repositories.Abstractions;
using DataLayer.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TrackYourLife.API.Infrastructure
{
    public static class DiContainer
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // Data Layer
            services.AddDbContext<AppDbContext>(
                opt => opt.UseSqlServer(ConfigurationConstants.DbConnection));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IDonorOrganRequestRepository, DonorOrganRequestRepository>();
            services.AddTransient<IOrganInfoRepository, OrganInfoRepository>();
            services.AddTransient<IClinicsRepository, ClinicsRepository>();
            services.AddTransient<IMedicalExamsRepository, MedicalExamsRepository>();
            services.AddTransient<IOrganDeliveryRepository, OrganDeliveryRepository>();
            services.AddTransient<IOrganDeliverySnapshotsRepository, OrganDeliverySnapshotsRepository>();


            // Business Layer
            services.AddTransient<ITokensService, TokensService>();
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
