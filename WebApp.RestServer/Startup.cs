using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Extensions;
using AspNetCore.Identity.Uow;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using DAL;
using DAL.App;
using DAL.EntityFrameworkCore;
using DAL.EntityFrameworkCore.Extensions;
using DAL.EntityFrameworkCore.Helpers;
using DAL.Helpers;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.RestServer.Filters;
using WebApp.RestServer.Services;

namespace WebApp.RestServer
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DAL.EntityFrameworkCore.ApplicationDbContext>(optionsAction: options =>
                options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "AppDbConnection")));

            services.AddScoped<IRepositoryProvider, EFRepositoryProvider<IDataContext>>();
            services.AddSingleton<IRepositoryFactory, EFRepositoryFactory>();

            services.AddScoped<IDataContext, DAL.EntityFrameworkCore.ApplicationDbContext>();
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork<IDataContext>>();
            services.AddScoped<IIdentityUnitOfWork<Domain.ApplicationUser>, ApplicationUnitOfWork<IDataContext>>();



            // ApplicationUser and IdentityRole have to come from our own models
            // check usages (no Microsoft.AspNetCore.Identity.EntityFrameworkCore);
            services.AddIdentity<Domain.ApplicationUser, IdentityRole>()
                .AddUnitOfWork<
                    IIdentityUserRepository<ApplicationUser>,
                    IIdentityRoleRepository,
                    IIdentityUserRoleRepository,
                    IIdentityUserLoginRepository,
                    IIdentityUserClaimRepository,
                    IIdentityUserTokenRepository,
                    IIdentityRoleClaimRepository>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .AddMvcOptions(setupAction: options =>
                {
                    options.Filters.Add(item: new AddCultureCookieFromQueryFilter());
                });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new[]{
                    new CultureInfo(name: "en-US"),
                    new CultureInfo(name: "en-GB"),
                    new CultureInfo(name: "et-EE")
                };

                // State what the default culture for your application is. 
                options.DefaultRequestCulture = new RequestCulture(culture: "en-GB", uiCulture: "en-GB");

                // You must explicitly state which cultures your application supports.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings
                options.SupportedUICultures = supportedCultures;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseRequestLocalization(
                options: app.ApplicationServices
                    .GetService<IOptions<RequestLocalizationOptions>>().Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var dataContext = app.ApplicationServices.GetService<DAL.EntityFrameworkCore.ApplicationDbContext>();
            if (dataContext != null)
            {
                // key in appsettings.Development.json
                if (Configuration.GetValue<Boolean>(key: "DropDatabaseAtStartup"))
                {
                    dataContext.Database.EnsureDeleted();
                }

                //dataContext.Database.EnsureCreated();

                dataContext.Database.Migrate();
                dataContext.EnsureSeedData();
            }

            app.UseStaticFiles();

            app.UseIdentity();



            // create default roles and users
            app.EnsureDefaultUsersAndRoles();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoue",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
