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
            services.AddTransient<IValuesRepository, ValuesRepository>();


            // Business Layer
            services.AddTransient<IValuesService, ValuesService>();
        }
    }
}
