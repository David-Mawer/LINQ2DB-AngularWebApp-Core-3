using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using LinqToDB.Data;
using AngularWebApp.DB;
using AngularWebApp.Auth.DB;
using AngularWebApp.Auth;
using IdentityServer4.Stores;

namespace AngularWebApp
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
            // Set up Linq2DB connection
            DataConnection.DefaultSettings = new Linq2dbSettings(Configuration);

            // configure app to use Linq2DB for the identity provider: BEGIN
            services.AddScoped<IUserStore<AspNetUsers>, AspNetUsersStore>();
            services.AddScoped<IRoleStore<AspNetRoles>, AspNetRolesStore>();
            services.AddScoped<IPersistedGrantStore, PersistedGrantStore>();
            // Tip: To access you database, inject DataConnection into the constructor, and cast it as type LinqDB in your constructor.
            //  to see this in action - look at DemoController.
            services.AddScoped<DataConnection, LinqDB>();
            services.AddTransient<IdentityRole<string>, AspNetRoles>();
            services.AddTransient<IdentityUserClaim<string>, AspNetUserClaims>();
            services.AddTransient<IdentityUserRole<string>, AspNetUserRoles>();
            services.AddTransient<IdentityUserLogin<string>, AspNetUserLogins>();
            services.AddTransient<IdentityUserToken<string>, AspNetUserTokens>();
            services.AddTransient<IdentityRoleClaim<string>, AspNetRoleClaims>();


            services.AddDefaultIdentity<AspNetUsers>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            });

            services.AddIdentityServer()
                .AddAspNetIdentity<AspNetUsers>()
                .AddIdentityResources()
                .AddApiResources()
                .AddClients()
                .AddSigningCredentials();
            // configure app to use Linq2DB for the identity provider: END

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200"); // Use this instead to use the angular cli server
                                                                                 // (then you must run "npm start" from the command line
                                                                                 //  before running the API in visual studio).
                }
            });
        }
    }
}
