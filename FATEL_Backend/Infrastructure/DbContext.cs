using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }

    public DbContext() //solely for mocking purposes
    {
        
    }
    
    //TODO: OnModelCreating
    //TODO: Table mapping for entities
}