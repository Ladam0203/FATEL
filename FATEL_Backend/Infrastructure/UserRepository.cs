using Application.Interfaces;
using Domain;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public User GetUserByUsername(string username)
    {
        return _context.UserTable.FirstOrDefault(u => u.Username == username) ?? throw new KeyNotFoundException("Username not found");
    }
}