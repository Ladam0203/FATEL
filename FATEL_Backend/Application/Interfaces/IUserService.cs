namespace Application.Interfaces;

public interface IUserService
{
    bool Login(string username, string password, out string token);
}