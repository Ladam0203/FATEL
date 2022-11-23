using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.UserTable.ToList();
    }

    public User Get(int id)
    {
        return _context.UserTable.FirstOrDefault(b => b.Id == id);    
    }
    
    public User GetUserByUsername(string username)
    {
        return _context.UserTable.FirstOrDefault(u => u.Username == username) ?? throw new KeyNotFoundException("Username not valid");
    }

    public void Add(User entity)
    {
        _context.UserTable.Add(entity);
        _context.SaveChanges();
    }

    public void Edit(User entity)
    {
        if(_context.UserTable.Find(entity.Id)==null)
            throw new KeyNotFoundException("User with id " + entity.Id + " does not exist");
        _context.ChangeTracker.Clear();
        _context.UserTable.Update(entity);
        _context.SaveChanges();
    }

    public void Remove(int id)
    {
        User user = _context.UserTable.Find(id);
        if (user != null)
        {
            _context.UserTable.Remove(user);
            _context.SaveChanges();
        }
    }
}