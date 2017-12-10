using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourLife.API.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using DataLayer.DbContext;
using System;
using System.Text;
using Common.Constants;
using Common.Utils;

namespace TrackYourLife.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc();

            DiContainer.AddCustomServices(services);

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddAuthentication()
                  .AddJwtBearer(cfg =>
                  {
                      cfg.RequireHttpsMetadata = false;
                      cfg.SaveToken = true;

                      cfg.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidIssuer = JwtConfigConstants.Issuer,
                          ValidateAudience = false,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LONG_LONG_HARD_KEY_1234567890"))
                      };
                  });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            string origins = "https://TanyaMy.github.io";
            app.UseCors(builder => builder
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseStaticFiles();
            app.UseAuthentication();
            
            app.Use(async (context, next) =>
            {
                CurrentUserHolder.CurrentUser = context?.User?.Identity?.Name;
                await next.Invoke();
            });

            app.UseMvc();
        }
    }
}
