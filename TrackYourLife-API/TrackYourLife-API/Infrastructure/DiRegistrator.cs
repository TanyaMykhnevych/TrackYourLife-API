using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourLife.API.Business.Services;
using TrackYourLife.API.Business.Services.Implementations;
using TrackYourLife.API.Data.Repositories;
using TrackYourLife.API.Data.Repositories.Implementations;

namespace TrackYourLife.API.Infrastructure
{
    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            // Data Layer
            services.AddTransient<IValuesRepository, ValuesRepository>();


            // Business Layer
            services.AddTransient<IValuesService, ValuesService>();
        }
    }
}
