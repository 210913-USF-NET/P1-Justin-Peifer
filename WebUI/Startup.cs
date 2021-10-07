using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DL;
using SBL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // transient, scoped, singleton
        //Transient: a new object is created every time you call it
        //Scoped: new object per request
        //Singleton:Share an instance across request -> could leead to other requests waiting
        //container that holds the resoiurces that the app needs
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddDbContext<P1DBContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("BeeliciousDB")));
            //secondly we needd to configure our ddb here, and add the db context as dependency
            //finally, we add all of the other dependdencies, such as BL, & Repos
            //This uses inversion of control, which measn that we specify what kinds of concrete classes implement interfaces.

            services.AddScoped<IRepo, DBRepo>();//goes from deepest level to shallowest
            services.AddScoped<IBL, BL>();
            //^Dependency injection. We are registering the dependencies that our controllers need
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

            app.UseCookiePolicy();
            app.UseSession();


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
