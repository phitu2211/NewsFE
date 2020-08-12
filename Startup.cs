using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NewsFE
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
            services.AddHttpClient("myApi", client =>
            {
                client.BaseAddress = new Uri("https://apinews.azurewebsites.net");
                //client.BaseAddress = new Uri("https://localhost:5001");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbkBhZG1pbi5jb20iLCJqdGkiOiI1OWZmZWI4My0xZTNkLTQ5MTktYjA4Zi02MWUzYzAyNzFlMTMiLCJlbWFpbCI6ImFkbWluQGFkbWluLmNvbSIsImlkIjoiYmI5NGU5MTUtYmRhZC00M2YwLWIzOWYtN2MxOTIyODdkMDFkIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNTk1Mzk5ODA2LCJleHAiOjE1OTc0NzM0MDYsImlhdCI6MTU5NTM5OTgwNn0.Gqav3ECJTNn1xHToKY61v5wZUuTUBoBJ7Z7jOAMhV34");
            });
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
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    //defaults: new {controller = "Home", action = "Index", id = UrlParameter}
                );
            });
        }
    }
}
