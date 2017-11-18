using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackYourLife.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using DataLayer.DbContext;
using DataLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.AccessTokenValidation;
using System.Collections.Generic;
using IdentityServer4.Test;
using IdentityServer4.Models;
using System.Security.Claims;
using System.Text;
using Common.Constants;
using Common.Utils;
using Microsoft.AspNetCore.Http;

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

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:4200")
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
