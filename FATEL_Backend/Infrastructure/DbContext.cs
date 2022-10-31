using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
    }
    
   
    #region Database sets
    public DbSet<Item> ItemTable { get; set; }
    #endregion
}