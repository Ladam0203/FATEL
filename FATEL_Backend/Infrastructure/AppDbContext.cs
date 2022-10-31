using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(item => item.Id)
            .ValueGeneratedOnAdd();
    }

    #region #region Database sets
    public DbSet<Item> ItemTable { get; set; }
    #endregion
}