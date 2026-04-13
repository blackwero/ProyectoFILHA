using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ApplicationDbContext context, IConfiguration config, ILogger<AuthController> logger)
    {
        _context = context;
        _config = config;
        _logger = logger;
    }

    private async Task GuardarLog(string nivel, string mensaje, string? usuario = null)
    {
        var log = new Log
        {
            Nivel = nivel,
            Mensaje = mensaje,
            Fecha = DateTime.Now,
            Usuario = usuario
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string correo, string password)
    {
        // 🪵 LOG (consola + BD)
        _logger.LogInformation("Intento de login con correo: {correo}", correo);
        await GuardarLog("INFO", "Intento de login", correo);

        var user = await _context.UsuariosApi
            .FirstOrDefaultAsync(u => u.Correo == correo);

        if (user == null)
        {
            _logger.LogWarning("Usuario no encontrado: {correo}", correo);
            await GuardarLog("WARNING", "Usuario no encontrado", correo);

            return Unauthorized("Credenciales incorrectas");
        }

        if (user.Password != password)
        {
            _logger.LogWarning("Password incorrecto para: {correo}", correo);
            await GuardarLog("WARNING", "Password incorrecto", correo);

            return Unauthorized("Credenciales incorrectas");
        }

        _logger.LogInformation("Login exitoso para: {correo}", correo);
        await GuardarLog("INFO", "Login exitoso", correo);

        var key = _config["Jwt:Key"];

        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Correo),
        new Claim("UserId", user.Id.ToString())
    };

        var keyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(keyBytes, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}