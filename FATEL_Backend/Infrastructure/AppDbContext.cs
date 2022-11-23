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
        //Item
        modelBuilder.Entity<Item>()
            .Property(item => item.Id)
            .ValueGeneratedOnAdd();
        
        //Warehouse
        modelBuilder.Entity<Entry>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        //Warehouse
        modelBuilder.Entity<Warehouse>()
            .Property(w => w.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Warehouse>()
            .HasMany(w => w.Inventory) //has many Items, but we call a list of items Inventory
            .WithOne(i => i.Warehouse)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Warehouse>()
            .HasMany(w => w.Diary) //has many Entries, but we call a list of entries Diary
            .WithOne(i => i.Warehouse)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        //User
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
    }

    #region #region Database sets
    
    public DbSet<Item> ItemTable { get; set; }
    public DbSet<Entry> EntryTable { get; set; }
    public DbSet<Warehouse> WarehouseTable { get; set; }
    public DbSet<User> UserTable { get; set; }
    
    #endregion
}