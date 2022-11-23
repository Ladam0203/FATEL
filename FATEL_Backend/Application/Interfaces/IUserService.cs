using Domain;

namespace Application.Interfaces;

public interface IUserService
{
    bool Login(string username, string password, out string token);
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    string GenerateToken(User user);
}