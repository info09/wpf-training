using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WPF_Learning.Core.Domains.Identity;
using WPF_Learning.Core.Domains.Systems;

namespace WPF_Learning.Model
{
    public class ApplicationContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Core.Domains.Systems.Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });

            builder.Entity<Assignment>().ToTable("Assignment").HasKey(x => new { x.ProjectId, x.EmployeeId });
        }
    }
}
