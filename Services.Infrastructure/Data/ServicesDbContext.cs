using Microsoft.EntityFrameworkCore;
using Services.Domain.Models;
using System.Reflection;

namespace Services.Infrastructure.Data;
public class ServicesDbContext : DbContext
{
    public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
    {
    }

    public DbSet<Service> services => Set<Service>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
