using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuplementsStore1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SuplementsStore1.Data;
using Microsoft.Extensions.Hosting;

namespace SuplementsStore1
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json")
                .Build();
        }//cita konekciju iz apssetings.json fajla i uspostvalja konekciju sa EFCore

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(
                                Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ISeedData, SeedData>();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); //govori da kada je potrebna instanca cart klase poziva
                                                                    //get cart metodu iz session cart klase
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedData seedData)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();

            app.UseSession();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("null", "{category}/Page{page:int}",
                    new { controller = "Product", action = "List" }
                    );
                routes.MapControllerRoute("null", "Page{page:int}",
                  new { controller = "Product", action = "List", page = 1 }
                  );
                routes.MapControllerRoute("null", "{category}",
                   new { controller = "Product", action = "List", page = 1 }
                   );
                routes.MapControllerRoute("null", "",
                   new { controller = "Product", action = "List", page = 1 }
                   );
                routes.MapControllerRoute("null", "{category}/Page{page:int}",
                   new { controller = "Product", action = "List" }
                   );
                routes.MapControllerRoute("null", "{controller}/{action}/{id?}");

            });
            seedData.EnsurePopulated();
        }
    }
}


