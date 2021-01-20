using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMovie.Helper;
using System.Collections.Generic;

namespace MvcMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // 注入 DB
            services.InJectionByDbContext(Configuration.GetConnectionString("MvcMovieContext"));

            //第一種版本
            // 注入 Repository
            // services.InJectionByGenericRepository();

            // 注入 Service
            // services.InJectionByService("Service");

            //第二種版本
            // 注入 Interface
            services.InJectionByInterface(new List<string>() { "Service", "Repository"});

            // 注入 Class
            services.InJectionByClass(new List<string>() { "Service", "Repository"});

            // 注入 Generic
            services.InJectionByGeneric("Model", new List<string>() { "Repository"});

            // 注入 UnitOfWork
            services.InJectionByUnitOfWork();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
