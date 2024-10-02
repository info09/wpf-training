﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WPF_Learning.Model
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            //builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            builder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            return new ApplicationContext(builder.Options);
        }
    }
}
