using Application.DTOs;
using Domain;

namespace Application.Interfaces;

public interface IUserService
{
    bool Login(string username, string password, out string token);
    bool CreateUser(LoginDTO dto);
}