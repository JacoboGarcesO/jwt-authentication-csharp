using JWTAuthentication.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Auth(LoginModel payload)
    {
      if (payload is null)
      {
        return BadRequest("Invalid client request");
      }

      if (payload.UserName == "marianita" && payload.Password == "marianita123")
      {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSecretKey"] ?? ""));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescription = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.UserData, payload.UserName),
          }),
          Expires = DateTime.UtcNow.AddMinutes(5),
          SigningCredentials = signinCredentials,
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new AuthenticatedResponse { Token = tokenString });
      }

      return Unauthorized();
    }
  }
}
