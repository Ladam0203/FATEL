using Application;
using Application.Interfaces;
using Domain;
using Moq;

namespace Test;

public class UserServiceTest
{
    [Fact]
    public void CreateUserService_WithNullRepository_ExpectArgumentNullException()
    {
        //Arrange
        IUserService userService = null;
        var mockHelper = new Mock<IAuthenticationHelper>();

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => userService = new UserService(null, mockHelper.Object));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'repositoryFacade')", e.Message);
        Assert.Null(userService);
    }
    
    [Fact]
    public void CreateUserService_WithNullHelper_ExpectArgumentNullException()
    {
        //Arrange
        IUserService userService = null;
        var mockRepository = new Mock<IRepositoryFacade>();

        //Act
        var e = Assert.Throws<ArgumentNullException>(() => userService = new UserService(mockRepository.Object, null));

        //Assert
        Assert.Equal("Value cannot be null. (Parameter 'authenticationHelper')", e.Message);
        Assert.Null(userService);
    }
    
    [Fact]
    public void CreateUserService_WithNonNullParameters()
    {
        //Arrange
        var mockRepository = new Mock<IRepositoryFacade>();
        var mockHelper = new Mock<IAuthenticationHelper>();

        //Act
        UserService userService = new UserService(mockRepository.Object, mockHelper.Object);

        //Assert
        Assert.NotNull(userService);
        Assert.True(userService is UserService);
    }

    [Fact]
    public void Login()
    {
        string username = "Username";
        string password = "password";
        string token = "";
        
        var mockRepository = new Mock<IRepositoryFacade>();
        var mockHelper = new Mock<IAuthenticationHelper>();

        User user = new User()
        {
            Id = 1,
            Username = "Username",
            PasswordHash = Array.Empty<byte>(),
            PasswordSalt = Array.Empty<byte>()
        };

        mockRepository.Setup(r => r.GetUserByUsername(username)).Returns(user);
        mockHelper.Setup(h => h.VerifyPasswordHash(password, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);
        mockHelper.Setup(h => h.GenerateToken(user)).Returns("token");

        UserService userService = new UserService(mockRepository.Object, mockHelper.Object);
        
        //Act
        bool login = userService.Login(username, password, out token);
        
        //Assert
        Assert.True(login);
        Assert.Equal("token", token);
        mockRepository.Verify(r => r.GetUserByUsername(username), Times.Once);
        mockHelper.Verify(h => h.VerifyPasswordHash(password, It.IsAny<byte[]>(), It.IsAny<byte[]>()), Times.Once);
        mockHelper.Verify(h => h.GenerateToken(user), Times.Once);
    }
    
}