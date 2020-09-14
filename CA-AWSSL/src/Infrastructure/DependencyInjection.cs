using System;
using System.Collections.Generic;
using Application.Common;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Okta.AspNetCore;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("b"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            var oktaMvcOptions = new OktaMvcOptions()
            {
                OktaDomain = configuration["Okta:Domain"],
                ClientId = configuration["Okta:ClientId"],
                ClientSecret = configuration["Okta:ClientSecret"],
                Scope = new List<string> {"openid", "profile", "email"},
            };
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
                })
                .AddCookie()
                .AddOktaMvc(oktaMvcOptions);

            return services;
        }
    }
}