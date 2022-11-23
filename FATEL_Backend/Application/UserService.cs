using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationHelper _authenticationHelper;
    

    public UserService(IUserRepository userRepository, IAuthenticationHelper authenticationHelper)
    {
        _userRepository = userRepository;
        _authenticationHelper = authenticationHelper;
        
    }
    
    public bool Login(string username, string password, out string token)
    {
        User user = _userRepository.GetAll().FirstOrDefault(user => user.Username.Equals(username));

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
    
    public bool CreateUser(LoginDTO dto)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Username == dto.Username);

        //Does already contain a user with the given username?
        if (user != null)
            return false;

        byte[] salt;
        byte[] passwordHash;
        _authenticationHelper.CreatePasswordHash(dto.Password, out passwordHash, out salt);

        user = new User()
        {
            Username = dto.Username,
            PasswordHash = passwordHash,
            PasswordSalt = salt
        };

        _userRepository.Add(user);

        return true;
    }
}