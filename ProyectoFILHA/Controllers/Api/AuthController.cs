using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly string key = string.Empty;

    public AuthController(IConfiguration config)
    {
        key = config["Jwt:Key"];
    }

    [HttpPost("login")]
    public IActionResult Login(string correo, string password)
    {
        // 🔥 Simulación (luego lo conectas a DB)
        if (correo == "admin@test.com" && password == "1234")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, correo)
            };

            var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        return Unauthorized();
    }
}