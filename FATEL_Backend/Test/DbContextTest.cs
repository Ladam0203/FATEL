using Microsoft.EntityFrameworkCore;

namespace Test;

//These are more like integration tests, as the server has to be running for these to pass
public class DbContextTest
{
    [Fact]
    public void Production_CanConnect_WithValidConnectionString()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContext>().UseNpgsql("Host=128.76.216.135;Database=FATEL_Production;Username=pi;Password=pi");
        
        DbContext dbContext = new DbContext(optionsBuilder.Options);

        Assert.True(dbContext.Database.CanConnect());
    }

    [Fact]
    public void Development_CanConnect_WithValidConnectionString()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContext>().UseNpgsql("Host=128.76.216.135;Database=FATEL_Development;Username=pi;Password=pi");
        
        DbContext dbContext = new DbContext(optionsBuilder.Options);

        Assert.True(dbContext.Database.CanConnect());
    }
    
    [Fact]
    public void CanConnect_WithInvalidConnectionString()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContext>().UseNpgsql("invalid");
        
        DbContext dbContext = new DbContext(optionsBuilder.Options);

        Assert.False(dbContext.Database.CanConnect());
    }
}