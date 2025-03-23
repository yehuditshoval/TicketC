using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket.Core.Models;
using Ticket.Core.Service;
using Ticket.Service;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowAllOrigins")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUsersService _usersService;

    public AuthController(IConfiguration configuration, IUsersService usersService)
    {
        _configuration = configuration;
        _usersService = usersService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var user = _usersService.GetAll().FirstOrDefault(u => u.Name == loginModel.UserName);

        if (user == null)
        {
            return Unauthorized("משתמש לא נמצא");
        }

        if (user.Password != loginModel.Password)
        {
            return Unauthorized("סיסמה שגויה");
        }

        var claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Role, "user"),
        new Claim("userId", user.Id.ToString()) 
    };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signinCredentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return Ok(new { Token = tokenString });
    }


// AuthController.cs
[HttpPost("signup")]
    public IActionResult Signup([FromBody] Users user)
    {
        if (user == null)
        {
            return BadRequest(new { error = "User data is required." });
        }

        var existingUser = _usersService.GetAll().FirstOrDefault(u => u.Name == user.Name);
        if (existingUser != null)
        {
            return BadRequest(new { error = "שם משתמש כבר קיים." });
        }



        _usersService.AddUser(user);

        var loginModel = new LoginModel { UserName = user.Name, Password = user.Password };

        return Login(loginModel);
    }




}