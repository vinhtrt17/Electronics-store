using Microsoft.EntityFrameworkCore;
using StoreManagement.IService;
using StoreManagement.Middleware;
using StoreManagement.Models;
using StoreManagement.Pages;
using StoreManagement.Services;
using System.Security.Cryptography.X509Certificates;

namespace StoreManagement
{
    public class Program
    {
        public IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<WebContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Web")));
            
            #region Services Scoped
            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<IProductDetailService, ProductDetailServices>();
            builder.Services.AddScoped<IColorDetailServices, ColorDetailServices>();
            builder.Services.AddScoped<IStorageDetailServices, StorageDetailServices>();
            builder.Services.AddScoped<IUsersManageServices, UsersManageService>();
            builder.Services.AddScoped<IOrderServices, OrderService>();
            builder.Services.AddScoped<IOrderDetailServices, OrderDetailService>();
            #endregion

            builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            
            app.UseMiddleware<CheckAccessMiddleware>();

            app.MapRazorPages();

            app.MapHub<StoreHub>("/storeHub");

            app.Run();
        }
    }
}