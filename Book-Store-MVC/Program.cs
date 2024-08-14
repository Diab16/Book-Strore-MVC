using Book_Store_MVC.IRepositories;
using Book_Store_MVC.MappingProfile;
using Book_Store_MVC.Models;
using Book_Store_MVC.Repositories;
using Day2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book_Store_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Adding session service
            builder.Services.AddSession(options =>
                options.IdleTimeout = TimeSpan.FromMinutes(25)
            );
            //Adding DatabaseContext Services
            builder.Services.AddDbContext<BookStoreContext>(option =>
            {

                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
                option.UseSqlServer(builder.Configuration.GetConnectionString("Youssef"));

                option.UseSqlServer(builder.Configuration.GetConnectionString("csLocal"));

                option.UseSqlServer(builder.Configuration.GetConnectionString("Alaa"));

            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

            //adding Di  "temp"
            builder.Services.AddScoped<BookStoreContext>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddScoped< BookRepository>();
            builder.Services.AddScoped< CategoryRepository>();

            builder.Services.AddScoped<IGenericRepository<Author> , AuthorRepository>();
            builder.Services.AddScoped<IGenericRepository<Category> , CategoryRepository>();
            builder.Services.AddScoped<IGenericRepository<Publisher>, PublisherRepository>();
            builder.Services.AddScoped<IGenericRepository<Category>, CategoryRepository>();







            builder.Services.AddAutoMapper(M => M.AddProfile(new BookMapProfile()));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
