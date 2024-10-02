using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.IO;
using System.Windows;
using WPF_Learning.Core.Domains.Identity;
using WPF_Learning.Core.SeedWorks;
using WPF_Learning.Data.SeedWorks;
using WPF_Learning.Model;

namespace WPF_Learning.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        //private IHost _host;

        public App()
        {
            _host.MigrateDatabase();
        }

        private IHost _host => Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register services
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

                context.Configuration = builder;

                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));

                services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
                services.AddScoped<UserManager<AppRole>, UserManager<AppRole>>();

                services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddScoped<ApplicationContext>();

                ServiceProvider = services.BuildServiceProvider();
            }).Build();
    }

}
