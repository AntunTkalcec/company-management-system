using CompanyManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Infrastructure.Database;

public class CompanyManagementSystemDBContext(DbContextOptions<CompanyManagementSystemDBContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Request> Requests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().Property(_ => _.Password)
            .UseCollation("SQL_Latin1_General_CP1_CS_AS");
    }
}
