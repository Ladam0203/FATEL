using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext() //solely for mocking purposes
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(item => item.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Entry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }

    #region #region Database sets
    public DbSet<Item> ItemTable { get; set; }
    public DbSet<Entry> EntryTable { get; set; }
    #endregion
}