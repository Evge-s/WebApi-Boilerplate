using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Aplication.Interfaces;

namespace Notes.Presistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection service,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            service.AddDbContext<NotesDbContext>(option =>
            {
                option.UseSqlite(connectionString);
            });
            service.AddScoped<INotesDbContext>(provider =>
                provider.GetService<NotesDbContext>());
            return service;
        }
    }
}
