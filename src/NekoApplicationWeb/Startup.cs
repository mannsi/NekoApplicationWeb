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
using NekoApplicationWeb.Data;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace NekoApplicationWeb
{
    public class Startup
    {
        readonly bool _isDevelopment;
        string databaseConnectionString;

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

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_isDevelopment)
            {
                databaseConnectionString = Configuration["NekoData:DebugConnection:ConnectionString"];
            }
            else
            {
                databaseConnectionString = Configuration["NekoData:DefaultConnection:ConnectionString"];
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(databaseConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 6;
                o.Cookies.ApplicationCookie.LoginPath = "/";
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddLogging();

            services.AddTransient<ICompletionService, CompletionService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IInterestsService, InterestsService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IThjodskraService, ThjodskraService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICostOfLivingService, CostOfLivingService>();
            services.AddTransient<ICreditScoreService, CreditScoreService>();
            services.AddTransient<IPropertyValuationService, PropertyValuationService>();
            services.AddTransient<IApplicationService, ApplicationService>();

            services.Configure<MailOptions>(myOptions =>
            {
                myOptions.SendGridApiKey = Configuration["NekoSendGridApiKey"];
            });

            services.AddSingleton(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Warning);

            var serilogLogger = new Serilog.LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.MSSqlServer(
                    connectionString: databaseConnectionString,
                    tableName: "Logs",
                    autoCreateSqlTable: true)
                .CreateLogger();

            loggerFactory.AddSerilog(serilogLogger);

            loggerFactory.CreateLogger("Startup").LogInformation("Starting up application");

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
                    template: "{controller=Account}/{action=MyFakeSignIn}/{id?}");
            });

            InitialData.CreateLenders(app.ApplicationServices);
            InitialData.CreateInterestInfo(app.ApplicationServices);
            //InitialData.CreateCostOfLivingEntries(app.ApplicationServices);
            //InitialData.ImportPropertyValuationData(app.ApplicationServices);
        }
    }
}
