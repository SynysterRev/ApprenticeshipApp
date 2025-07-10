using ApprenticeshipApp.Domain.Repositories;
using ApprenticeshipApp.Infrastructure.Repositories;

namespace ApprenticeshipApp.WebAPI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<ISessionsRepository, SessionsRepository>();

            return services;
        }
    }
}
