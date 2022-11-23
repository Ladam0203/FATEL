using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    User Get(int id);
    IEnumerable<User> GetAll();
    User GetUserByUsername(string username);
    void Edit(User entity);
    void Remove(int id);
}