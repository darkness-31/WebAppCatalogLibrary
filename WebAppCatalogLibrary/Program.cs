using Microsoft.EntityFrameworkCore;
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
