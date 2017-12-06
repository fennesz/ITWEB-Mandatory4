using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL.repos;
using System.Security.Cryptography;
using System.Text;
using webapi.DAL.models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace webapi.Controllers
{
  [Route("api/auth")]
  public class AuthenticationController : Controller
  {
    UserRepo _repo;

    public AuthenticationController(UserRepo repo)
    {
      _repo = repo;
    }

    [HttpPost("register")]
    public IActionResult Create([FromBody]RegisterModel user)
    {
      ActionResult res = null;
      var existingUserName = _repo.Find(u => u.Name == user.Username).Any();
      if (existingUserName)
      {
        // User exists
        res = Json(new { err = "Username or Email already taken" });
      }
      else
      {
        // Create user
        var rand = RandomNumberGenerator.Create();
        byte[] randBytes = new byte[24];
        rand.GetBytes(randBytes);
        var salt = Encoding.UTF8.GetString(randBytes);
        var newUser = new User
        {
          Email = user.Email,
          Name = user.Username,
          HashedPassword = SaltAndHashPassword(user.Password, salt),
          Salt = salt
        };
        _repo.Insert(newUser);
        res = Json(new { });
      }
      return res;
    }

    [HttpPost("token")]
    public IActionResult Login([FromBody]LoginModel login)
    {
      ActionResult res = null;

      var user = _repo.Find(u => u.Name == login.Username).First();
      var hashedPassword = SaltAndHashPassword(login.Password, user.Salt);

      if (hashedPassword == user.HashedPassword)
      {
        // Password correct
        var expirationData = DateTime.Now + TimeSpan.FromDays(1);

        /////////
        var secret = Environment.GetEnvironmentVariable("Secret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

        var claimsIdentity = new ClaimsIdentity(new List<Claim>()
          {
              new Claim("userID", user._id),
              new Claim("exp", DateTimeToSeconds(expirationData).ToString()),
              new Claim("iat", DateTimeToSeconds(DateTime.Now).ToString())
          }, "Custom");

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
          Issuer = "BackEnd",
          Audience = "SPA",
          Subject = claimsIdentity,
          SigningCredentials = signingCredentials,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
        var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
        res = Json(new { Token = signedAndEncodedToken });
        ////////
      }
      else
      {
        // Password wrong
        res = Json(new { err = "Wrong credentials" });
      }

      return res;
    }

    private string SaltAndHashPassword(string password, string salt)
    {
      var sha = SHA512.Create();
      var saltedPassword = password + salt;
      var hashedPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
      return Encoding.UTF8.GetString(hashedPassword);
    }

    private int DateTimeToSeconds(DateTime time)
    {
      DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);//from 1970/1/1 00:00:00 to now
      TimeSpan result = time.Subtract(dt);
      int seconds = Convert.ToInt32(result.TotalSeconds);
      return seconds;
    }

    public class RegisterModel
    {
      public string Username { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
    }

    public class LoginModel
    {
      public string Username { get; set; }
      public string Password { get; set; }
    }
  }
}
