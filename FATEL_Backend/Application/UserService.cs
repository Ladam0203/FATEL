using System.Security.Claims;
using Application.Interfaces;
using Domain;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            token = null;
            return false;
        }

        token = GenerateToken(user);
        return true;
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
        }
        return true;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("name", user.Username),
            new Claim("id", user.Id.ToString())
        };
        
        var token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(
                new SymmetricSecurityKey(secretBytes),
                SecurityAlgorithms.HmacSha256)),
            new JwtPayload(null, // issuer - not needed (ValidateIssuer = false)
                null, // audience - not needed (ValidateAudience = false)
                claims.ToArray(), //I add the claims to the token!
                DateTime.Now, // notBefore
                DateTime.Now.AddMinutes(10))); // expires

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    }
}