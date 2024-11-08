using Microsoft.AspNetCore.Mvc;
using secure_online_bookstore.Services;
using secure_online_bookstore.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly JwtSettings jwtSettings;
    private static List<RegisterUser> registerUsers = new List<RegisterUser>();
    private readonly IUserService _userService;
    public UserController(IOptions<JwtSettings> options, IUserService userService)
    {
        this.jwtSettings = options.Value;
        _userService = userService;
    }


    [HttpPost]
    public ActionResult<string> Login([FromBody]User user)
    {
        try
        {
            return Ok(_userService.Login(user));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 

    [HttpPost("Register")]
    public ActionResult<string> Register(RegisterUserDto registerUser)
    {
        try
        {
            if(String.IsNullOrEmpty(registerUser.FirstName) || String.IsNullOrEmpty(registerUser.LastName) || String.IsNullOrEmpty(registerUser.Email)
            || String.IsNullOrEmpty(registerUser.UserName) || String.IsNullOrEmpty(registerUser.Password) || String.IsNullOrEmpty(registerUser.PasswordVerify))
            {
                return "All fields are required!";
            }
            return Ok(_userService.Register(registerUser));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}