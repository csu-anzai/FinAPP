using AutoMapper;
using BLL.DTOs;
using DAL.Context;
using FinApp.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System.Globalization;
using System.Linq;

namespace FinApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FinAppContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection"]));

            services.AddLocalization(o => o.ResourcesPath = "Resources");

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("de"),
            //    new CultureInfo("fr"),
            //};


            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //    options.RequestCultureProviders = new[] { new CookieRequestCultureProvider { CookieName = "language" } };
            //});

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
               {
                new CultureInfo("en"),
                new CultureInfo("de"),
                new CultureInfo("fr"),
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                var defaultCookieRequestProvider =
                    options.RequestCultureProviders.FirstOrDefault(rcp =>
                        rcp.GetType() == typeof(CookieRequestCultureProvider));

                if (defaultCookieRequestProvider != null)
                    options.RequestCultureProviders.Remove(defaultCookieRequestProvider);

                options.RequestCultureProviders.Insert(0,
                    new CookieRequestCultureProvider()
                    {
                        CookieName = "language_for_checking",
                        Options = options
                    });
            });

            services.AddAutoMapper();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureDataAccessServices();

            services.Configure<EmailOptionsDTO>(Configuration.GetSection("EmailSettings"));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fin Web API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinApp");
            });

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
            app.UseGlobalExcepionMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOptions.Value);

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //   name: "api",
                //   template: "api/{controller}/{action}");

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
