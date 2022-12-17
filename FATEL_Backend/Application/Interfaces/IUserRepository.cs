using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    User GetUserByUsername(string username);

}