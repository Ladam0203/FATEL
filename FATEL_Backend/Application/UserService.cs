using Application.Interfaces;
using Domain;

namespace Application;

public class UserService : IUserService
{
    private readonly IRepositoryFacade _repositoryFacade;
    private readonly IAuthenticationHelper _authenticationHelper;
    

    public UserService(IRepositoryFacade repositoryFacade, IAuthenticationHelper authenticationHelper)
    {
        _repositoryFacade = repositoryFacade ?? throw new ArgumentNullException(nameof(repositoryFacade));
        _authenticationHelper = authenticationHelper?? throw new ArgumentNullException(nameof(authenticationHelper));
        
    }
    
    public bool Login(string username, string password, out string token)
    {
        User user = _repositoryFacade.GetUserByUsername(username);

        //Does user exist?
        if (user == null)
        {
            token = null;
            return false;
        }

        //Correct password?
        if (!_authenticationHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            token = null;
            return false;
        }

        token = _authenticationHelper.GenerateToken(user);
        return true;
    }
}