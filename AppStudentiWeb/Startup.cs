using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppStudentiCommon;
using AppStudentiCommon.Interface;
using AppStudentiBusiness;
using AppStudentiBusiness.Mock;
using AppStudentiData;

namespace AppStudentiWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<appstudentiContext>(options => options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));

            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddTransient<IProvincesManager, ProvincesMockManager>();
            //services.AddTransient<ITeachersManager, TeachersMockManager>();
            //services.AddTransient<IStudentsManager, StudentsMockManager>();
            //services.AddTransient<ICoursesManager, CoursesMockManager>();
            //services.AddTransient<IWorkshopsManager, WorkshopsMockManager>();

            services.AddTransient<IProvincesManager, ProvincesManager>();
            services.AddTransient<ITeachersManager, TeachersManager>();
            services.AddTransient<IStudentsManager, StudentsManager>();
            services.AddTransient<ICoursesManager, CoursesManager>();
            services.AddTransient<IWorkshopsManager, WorkshopsManager>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var cultureInfo = new CultureInfo("it-IT");
            //cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
            //cultureInfo.NumberFormat.CurrencyDecimalSeparator = ",";
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(cultureInfo),
            //    SupportedCultures = new List<CultureInfo>
            //    {
            //        cultureInfo,
            //    },
            //    SupportedUICultures = new List<CultureInfo>
            //    {
            //        cultureInfo,
            //    }
            //});
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
