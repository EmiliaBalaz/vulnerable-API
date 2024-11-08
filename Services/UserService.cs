using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using secure_online_bookstore.Models;
using secure_online_bookstore.Data;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.RegularExpressions;

namespace secure_online_bookstore.Services
{
    
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>();
        private static string encryptedPassword;
        private readonly IEncryptService _encrypteService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(IEncryptService encryptService, DataContext context, IMapper mapper)
        {
            _encrypteService = encryptService;
            _context = context;
            _mapper = mapper;
        }

        public string Login(User user)
        {
            var _user = _context.RegisterUsers.ToList().FirstOrDefault(u=>u.UserName == user.UserName);

           encryptedPassword = _encrypteService.EncodePassword(user.Password);
            if(_user is null)
            {
                throw new Exception("User is not found. The username is not correct.");
            }
            if(_user.Password != encryptedPassword)
            {
                throw new Exception("Incorrect password!");

            }
            if(ValidateUserName(_user.UserName) == false)
            {
                throw new Exception("Your username is incorrect.");
            }

            List<Claim> claims = new List<Claim>();            
            if(_user.Type == UserType.StuffMember)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if(_user.Type == UserType.Visitor)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Visitor"));
            }
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
            //Generate Token
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes("thisismysecurekey");
            var tokendescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256),
                Issuer = "https://localhost:7102",
                Audience = "https://localhost:7102"
            };
            var token = tokenhandler.CreateToken(tokendescriptor);
            string finalToken = tokenhandler.WriteToken(token);

            return finalToken;
        }
        public string Register(RegisterUserDto registerUser)
        {
            RegisterUser reguser = new RegisterUser();
            if(ValidateFirstAndLastName(registerUser.FirstName) == false)
            {
                throw new Exception("Incorrect FirstName.");
            }
            reguser.FirstName = registerUser.FirstName;
            if(ValidateFirstAndLastName(registerUser.LastName) == false)
            {
                throw new Exception("Incorrect LastName.");
            }
            reguser.LastName = registerUser.LastName;
            if(ValidateEmailAddress(registerUser.Email) == false)
            {
                throw new Exception("Please enter your email address in format: yourname@example.com!");
            }

            reguser.Email = registerUser.Email;
            if(ValidateUserName(registerUser.UserName) == false)
            {
                throw new Exception("Numerics and spec characters are not allowed.");
            }
            reguser.UserName = registerUser.UserName;
            if(ValidatePassword(registerUser.Password) == false)
            {
                throw new Exception("Weak password! \nPlease use:\nAt least 8 characters\nUpper and lower case characters\n1 ore more numbers\nOptionally special characters\n");
            }
            if(registerUser.Password != registerUser.PasswordVerify)
            {
                 throw new Exception("Incorrect passwords!");
            }
            reguser.Password = _encrypteService.EncodePassword(registerUser.Password);
            reguser.Type = UserType.Visitor;

            var _registerUser = _mapper.Map<RegisterUser>(reguser);
            _context.Add<RegisterUser>(_registerUser);
            _context.SaveChanges();
            string message = "Success!";
            return message;
        }

        public bool ValidatePassword(string password)
        {
            bool passLength = password.Length < 8;
            bool oneUpperCase = !password.Any(char.IsUpper);
            bool oneLowerCase = !password.Any(char.IsLower);
            bool whiteSpace = password.Contains(" ");
            if(passLength == true ||  oneUpperCase == true || oneLowerCase == true || whiteSpace == true)
            {
                return false;
            }
            return true;
        }
        public bool ValidateEmailAddress(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                return email.Address == emailAddress.Trim();
            }
            catch
            {
                return false;
            }
        }

        public bool ValidateUserName(string userName)
        {
            bool matchUserName = Regex.IsMatch(userName, @"[a-zA-Z]+$");
            bool lengthofuserName = userName.Length < 1 || userName.Length > 50;
            if(matchUserName == false || userName=="" || lengthofuserName == true)
            {
                return false;
            }
               
            return true;
        }

        public bool ValidateFirstAndLastName(string name)
        {
            bool matchName = Regex.IsMatch(name, @"[a-zA-Z]+[a-zA-Z]*");
            bool lengthofname = name.Length < 1 || name.Length > 50;
            if(matchName == false || name=="" || lengthofname == true)
            {
                return false;
            }
            return true;
        }
    }
}