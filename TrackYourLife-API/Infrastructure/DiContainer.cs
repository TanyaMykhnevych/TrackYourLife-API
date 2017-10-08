using BusinessLayer.Services.Abstractions;
using BusinessLayer.Services.Implementations;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using DataLayer.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace TrackYourLife.API.Infrastructure
{
    public static class DiContainer
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            // Data Layer
            services.AddDbContext<AppDbContext>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IDonorOrganRequestRepository, DonorOrganRequestRepository>();
            services.AddTransient<IOrganInfoRepository, OrganInfoRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IClinicsRepository, ClinicsRepository>();


            // Business Layer
            services.AddTransient<ITokensService, TokensService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IDonorOrganRequestService, DonorOrganRequestService>();
            services.AddTransient<IOrganInfoService, OrganInfoService>();
            services.AddTransient<ITransplantOrgansService, TransplantOrgansService>();
            services.AddTransient<IClinicsService, ClinicsService>();
            services.AddTransient<IPatientOrganRequestService, PatientOrganRequestService>();
            services.AddTransient<IPatientQueueService, PatientQueueService>();
        }
    }
}
