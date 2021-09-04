using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.MVC.AutoMapper.Profiles;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extentions;
using System.Text.Json.Serialization;

namespace ProgrammersBlog.MVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // For our changes to display on the browser page concurrently, we must use the RazorRuntimeCompilation service.
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                // For returning JSON typed results, we must use JsonSerializer options.
                // Set the JsonSerializer for startup service.
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddSession(); // Adding session for storing user information in global variables.
            // Adding AutoMapper library for the matching two objects
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile));
            services.LoadMyServices(); // Load all services.
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login"); // Auto routing for non-logged in users.
                options.LogoutPath = new PathString("/Admin/User/Logout"); // Auto routing for logged out users.
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog", // Name of the cookie
                    HttpOnly = true,  // For preventing XSS attacks
                    SameSite = SameSiteMode.Strict, // For preventing CSRF attacks request ----> hacker ----> server
                    SecurePolicy = CookieSecurePolicy.SameAsRequest // HTTP <=> HTTP, HTTPS <=> HTTPS (always HTTPS is the best choice)
                };
                options.SlidingExpiration = true;  // Remember me option
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7); // User cookie information stored 7 days in the system.
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");  // If the user doesn't have access to the page (403).
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages(); // Use HTTP error pages in this project.
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); // This configuration is all about verifying and recognizing users in the system. (Which user?)
            app.UseAuthorization();  // Which roles does an authenticated user have?
            app.UseEndpoints(endpoints =>
            {
                // Defining Admin-Area.
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
