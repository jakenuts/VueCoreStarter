using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Westwind.AspNetCore.LiveReload;

namespace VueCoreStarter
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
            //
            // Adds 'live reload' middleware from Rick Strahl. 
            // Configurable in appsettings.config or with environmment 
            // variable "LiveReload:LiveReloadEnabled"
            // https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
            services.AddLiveReload();

            //
            // Adds MVC controller/view services and these commonly used features:
            //      AddApiExplorer, AddAuthorization, AddCors, AddDataAnnotations,
            //      AddFormatterMappings, AddCacheTagHelper, AddViews, AddRazorViewEngine,
            //      and AddNewtonsoftJson
            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();

#if USEOPENAPI
            //
            // # Add api documentation with NSwag
            //
            services.AddOpenApiDocument();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // IMPORTANT: Before **any other output generating middleware** handlers including error handlers
            // If not enabled in appsettings.config or environment variables this line has no effect.
            app.UseLiveReload();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHttpsRedirection();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseStaticFiles();

#if USEOPENAPI
            //
            // Add api documentation with NSwag at ~/swagger 
            //
            app.UseOpenApi();
            app.UseSwaggerUi3();
#endif
            
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
