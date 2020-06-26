using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeCRUDApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeCRUDApp
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlDataContractSerializerFormatters();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            services.Configure<IdentityOptions>(options=> {
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            });
            //services.AddMvcCore();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 10
                };
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                //app.UseStatusCodePages();
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
            }

            /*app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                //await context.Response.WriteAsync("Hello from the first middleware");
                logger.LogInformation("Mw1 : Incoming request.");
                await next();
                logger.LogInformation("Mw1 : Outgoing request.");
            });
            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                //await context.Response.WriteAsync("Hello from the first middleware");
                logger.LogInformation("Mw2 : Incoming request.");
                await next();
                logger.LogInformation("Mw2 : Outgoing request.");
            });*/
            /*DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("razor.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();*/
            /*FileServerOptions fileServerOptions = new FileServerOptions();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("razor.html");
            app.UseFileServer(fileServerOptions);*/

            /*app.UseFileServer();*/
            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();
            // Conventional Routing
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc();

            /*app.Run(async (context) =>
            {
                //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                //throw new Exception("Some processing error occured");
                //await context.Response.WriteAsync("Name of the environment : " + env.EnvironmentName + " " + env.ContentRootPath);
                await context.Response.WriteAsync("Hello World");
                //logger.LogInformation("Mw3 : Request handled and response produced.");
            });*/
        }
    }
}
