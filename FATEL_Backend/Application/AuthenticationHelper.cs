using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Interfaces;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public class AuthenticationHelper : IAuthenticationHelper
{
    
    private byte[] secretBytes;

    public AuthenticationHelper(Byte[] secret)
    {
        secretBytes = secret;
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