using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Blog.Db.Controllers;
using Blog.Models.DbModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SignalRChat.Hubs;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IServiceProvider __serviceProvider;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IndentityDbContext>(options =>
                options.UseSqlite(Configuration["Connections:IndentityContext"]));

            services.AddIdentity<UserIndentity, IdentityRole>()
                    .AddEntityFrameworkStores<IndentityDbContext>()
                    .AddDefaultTokenProviders();

            ConfigureDb(services);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings>
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
                {
                    var supportedCultures = new[]
                    {
                            new CultureInfo("en"),
                            new CultureInfo("de"),
                            new CultureInfo("ru"),
                            new CultureInfo("en-CA"),
                            new CultureInfo("be")
                    };

                    options.DefaultRequestCulture = new RequestCulture("en");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                });

            

            services.AddSignalR();
            //COSTIL!!!
            __serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureDb(IServiceCollection services)
        {       
            services.AddDbContext<BlockPostContext>(options =>
                options.UseSqlite(Configuration["Connections:BlockPostContext"]));

            services.AddDbContext<CommentContext>(options =>
                options.UseSqlite(Configuration["Connections:CommentContext"]));

            services.AddDbContext<LikeContext>(options =>
                options.UseSqlite(Configuration["Connections:LikeContext"]));
        
            services.AddDbContext<MessageContext>(options =>
                options.UseSqlite(Configuration["Connections:MessageContext"]));

            services.AddDbContext<MessageBoxContext>(options =>
                options.UseSqlite(Configuration["Connections:MessageBoxContext"]));

            services.AddDbContext<PostContext>(options =>
                options.UseSqlite(Configuration["Connections:PostContext"]));

            services.AddDbContext<RepostContext>(options =>
                options.UseSqlite(Configuration["Connections:RepostContext"]));

            services.AddDbContext<TagContext>(options =>
                options.UseSqlite(Configuration["Connections:TagContext"]));

                //layers
            services.AddDbContext<MessageBoxLayerContext>(options =>
                options.UseSqlite(Configuration["Connections:MessageBoxLayerContext"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Settings HTTPS
            //https://blogs.msdn.microsoft.com/webdev/2017/11/29/conafiguring-https-in-asp-net-core-across-different-platforms/
            int? httpsPort = null;
            var httpsSection = Configuration.GetSection("HttpServer:Endpoints:Https");
            if (httpsSection.Exists())
            {
                var httpsEndpoint = new EndpointConfiguration();
                httpsSection.Bind(httpsEndpoint);
                httpsPort = httpsEndpoint.Port;
            }
            var statusCode = env.IsDevelopment() ? StatusCodes.Status302Found : StatusCodes.Status301MovedPermanently;
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode, httpsPort));

            //Coockie check
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
 

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes => 
            {
                routes.MapHub<ChatHub>("/chathub");
            });
            
        }
    }
}
