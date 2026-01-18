using Edukate_simulation_AzMPA201.Data;
using Edukate_simulation_AzMPA201.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Edukate_simulation_AzMPA201
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();


            //        var connectionString =
            //builder.Configuration.GetConnectionString("DefaultConnection")
            //    ?? throw new InvalidOperationException("Connection string"
            //    + "'DefaultConnection' not found.");

            //        builder.Services.AddDbContext<AppDbContext>(options =>
            //            options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //    options.Password.RequiredLength = 4;
                //    options.Password.RequireUppercase = false;



            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
        );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");





            app.Run();
        }
    }
}
