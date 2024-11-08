using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;

namespace secure_online_bookstore.Services
{
    public interface IUserService
    {
        string Login(User user);
        string Register(RegisterUserDto registerUser);
    }
}