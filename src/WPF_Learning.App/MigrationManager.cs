using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF_Learning.Model;

namespace WPF_Learning.App
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();

            new ApplicationContextSeed().SeedAsync(context).GetAwaiter().GetResult();

            return host;
        }
    }
}
