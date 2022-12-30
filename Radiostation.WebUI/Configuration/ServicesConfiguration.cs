using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Radiostation.DAL;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;

namespace Radiostation.WebUI.Configuration
{
    public static class ServicesConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<RadiostationContext>(x => x.UseSqlServer(connection));

            serviceCollection.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
            serviceCollection.AddScoped<IBaseRepository<Genre>, GenreRepository>();
            serviceCollection.AddScoped<IBaseRepository<Performer>, PerformerRepository>();
            serviceCollection.AddScoped<IBaseRepository<Track>, TrackRepository>();
            serviceCollection.AddScoped<IBaseRepository<Translation>, TranslationRepository>();
        }
    }
}
