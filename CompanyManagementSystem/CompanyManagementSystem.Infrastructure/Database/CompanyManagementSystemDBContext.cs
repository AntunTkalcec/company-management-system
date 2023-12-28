using CompanyManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Infrastructure.Database;

public class CompanyManagementSystemDBContext(DbContextOptions<CompanyManagementSystemDBContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
