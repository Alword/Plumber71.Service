using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Slepushko.Data;

namespace Slepushko
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = new ConfigurationBuilder()
                     .AddJsonFile("appsettings.json")
                     .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json")
                     .AddJsonFile($"appsettings.secret.json")
                     .Build();
        }
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PlumberContext>();
                using (PlumberContext context = new PlumberContext())
                {
                    context.ServiceTitles.Add(new ServiceTitle()
                    {
                        Title = "������ � ����� ����",
                        Description = "��������� � ������ ������ ��������������� ������������",
                        Heigth = 100,
                        Width = 100,
                        Url = "/img/service-set-1.png"
                    });
                    context.SaveChanges();
                }
            }
            else
            {
                services.AddDbContext<PlumberContext>(d => d.UseMySql(Configuration.GetConnectionString("Default")));
            }

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-Fr"),
                new CultureInfo("ru-RU"),
            };

            var requestLocalizationOptions = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });


        }
    }
}
