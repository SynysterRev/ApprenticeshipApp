using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.Repositories;

namespace JuniorOnly.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IOffersRepository, OffersRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<ICandidateProfileRepository, CandidateProfileRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}

// If any reference to ApprenticeshipApp, rename to JuniorOnly
