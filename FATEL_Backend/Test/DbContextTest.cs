using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Test;

//These are more like integration tests, as the server has to be running for these to pass
public class DbContextTest
{
    /*
     * Tested valid connection strings both for production and development, which are passed, however they were removed, not to expose connection strings
     */
    
    [Fact]
    public void CanConnect_WithInvalidConnectionString()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql("invalid");
        
        var dbContext = new AppDbContext(optionsBuilder.Options);

        Assert.False(dbContext.Database.CanConnect());
    }
}