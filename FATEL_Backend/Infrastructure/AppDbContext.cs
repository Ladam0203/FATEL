using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public AppDbContext() //solely for mocking purposes
    {
        
    }
    
    //TODO: OnModelCreating
    //TODO: Table mapping for entities
}