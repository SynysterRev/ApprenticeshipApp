using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.Services;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using JuniorOnly.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JuniorOnly.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            var password = configuration["DB_PASSWORD"];
            connectionString = connectionString?.Replace("{DB_PASSWORD}", password);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ICandidateProfileRepository, CandidateProfileRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobSectorRepository, JobSectorRepository>();

            // Services
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ICandidateProfileService, CandidateProfileService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobSectorService, JobSectorService>();

            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            var defaultOrigins = new string[] { "http://localhost:4200" };
            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    // Add angular address
                    builder
                    .WithOrigins(allowedOrigins ?? defaultOrigins)
                    .WithHeaders("Authorization", "origin", "accept", "content-type")
                    .WithMethods("GET", "POST", "PUT", "DELETE");
                });
            });

            services.AddTransient<IJwtService, JwtService>();

            // Authentication JWT
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateAudience = true,
            //        ValidAudience = configuration["Jwt:Audience"],
            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            //    };
            //});
        }
    }
}
