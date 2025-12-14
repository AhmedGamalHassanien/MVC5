using Business_Logic_Layer;
using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.Base;
using Data_Access_Layer.Context;
using Data_Access_Layer.Entites;
using Data_Access_Layer.Repositories;
using Data_Access_Layer.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Presentation_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);

            builder.Services.AddDbContext<CompanyDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //app.UseRouting();
            //app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
