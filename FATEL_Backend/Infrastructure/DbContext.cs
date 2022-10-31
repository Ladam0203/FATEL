using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
    
    //TODO: OnModelCreating

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
    }
    
    #endregion
   
    #region TableMapping
    public DbSet<Item> ItemTable { get; set; }
    #endregion
    //TODO: Table mapping for entities
}