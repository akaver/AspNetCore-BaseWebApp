using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow;
using AspNetCore.Identity.Uow.Interfaces;
using AspNetCore.Identity.Uow.Models;
using DAL;
using DAL.EntityFrameworkCore;
using DAL.EntityFrameworkCore.Extensions;
using DAL.EntityFrameworkCore.Helpers;
using DAL.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using WebApp.Services;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath: env.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true);

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
            services.AddDbContext<ApplicationDbContext>(optionsAction: options =>
                options.UseSqlServer(connectionString: Configuration.GetConnectionString(name: "AppDbConnection")));

            services.AddScoped<IDataContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork<IDataContext>>();
            services.AddScoped<IIdentityUnitOfWork, UnitOfWork<IDataContext>>();
            services.AddScoped<IRepositoryProvider, EFRepositoryProvider<IDataContext>>();
            services.AddSingleton<IRepositoryFactory, EFRepositoryFactory>();


            var x = typeof(UserStore<,,,,,,,,,,,,,>).MakeGenericType(
                typeof(int), //TKey
                typeof(IdentityUser),
                typeof(IdentityRole),
                typeof(IdentityUserClaim<int>), //TUserClaim
                typeof(IdentityUserRole<int>), //TUserRole
                typeof(IdentityUserLogin<int>), //TUserLogin
                typeof(IdentityUserToken<int>), //TUserToken
                typeof(IdentityRoleClaim<int>), //TRoleClaim from roletype
                typeof(IIdentityUserRepository),
                typeof(IIdentityRoleRepository),
                typeof(IIdentityUserRoleRepository<IdentityUserRole<int>>),
                typeof(IIdentityUserLoginRepository<IdentityUserLogin<int>>),
                typeof(IIdentityUserClaimRepository<IdentityUserClaim<int>>),
                typeof(IIdentityUserTokenRepository<IdentityUserToken<int>>)
            );

            //services.TryAddScoped<IUserStore<IdentityUser>, 
            //    UserStore<int, IdentityUser, IdentityRole, IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityUserToken, IdentityRoleClaim, 
            //    IIdentityUserRepository, IIdentityRoleRepository, IIdentityUserRoleRepository, IIdentityUserLoginRepository, IIdentityUserClaimRepository, IIdentityUserTokenRepository>>();



            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddUnitOfWork<
                    IIdentityUserRepository,
                    IIdentityRoleRepository,
                    //IIdentityUserRepository<int, IdentityUser>,
                    //IIdentityRoleRepository<int, IdentityRole>,
                    IIdentityUserRoleRepository,
                    IIdentityUserLoginRepository,
                    IIdentityUserClaimRepository,
                    IIdentityUserTokenRepository,
                    IIdentityRoleClaimRepository>()
                .AddDefaultTokenProviders();



            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(configuration: Configuration.GetSection(key: "Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
            }


            var dataContext = app.ApplicationServices.GetService<ApplicationDbContext>();
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

            app.UseMvc(configureRoutes: routes =>
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
