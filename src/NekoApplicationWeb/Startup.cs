using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NekoApplicationWeb.Models;
using NekoApplicationWeb.Services;
using NekoApplicationWeb.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace NekoApplicationWeb
{
    public class Startup
    {
        readonly bool _isDevelopment;

        public Startup(IHostingEnvironment env)
        {
            _isDevelopment = env.IsDevelopment();

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString;
            if (_isDevelopment)
            {
                connectionString = Configuration["NekoData:DebugConnection:ConnectionString"];
            }
            else
            {
                connectionString = Configuration["NekoData:DefaultConnection:ConnectionString"];
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
                o.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            services.AddTransient<ICompletionService, CompletionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IInterestsService, InterestsService>();
            services.AddTransient<ILoanService, LoanService>();

            services.Configure<MailOptions>(myOptions =>
            {
                myOptions.SendGridApiKey = Configuration["NekoSendGridApiKey"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Application/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity(); // This allows asp login
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=StartPage}/{id?}");
            });

            await InitialData.CreateDemoUser(app.ApplicationServices);
            InitialData.CreateLenders(app.ApplicationServices);
        }
    }
}
