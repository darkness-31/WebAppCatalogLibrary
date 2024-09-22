using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using WebAppCatalogLibrary.DataBase.Models;
using WebAppCatalogLibrary.DataBase.Repositories;

namespace WebAppCatalogLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var configDbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection:PostgreSql");
            builder.Services.AddDbContext<DbPostgresContext>(options => options.UseNpgsql(configDbConnectionString));

            builder.Services.AddTransient<IRepository<Book>, BookRepository>();
            builder.Services.AddTransient<IRepository<Author>, AuthorRepository>();
            builder.Services.AddTransient<IRepository<BookSection>, SectionRepository>();
            builder.Services.AddTransient<IRepository<AssociationBookAuthor>, AssociationBookAuthorRepository>();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DbPostgresContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
