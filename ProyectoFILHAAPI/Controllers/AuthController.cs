using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.API.Data;
using ProyectoFILHAPI.DTOs;

namespace ProyectoFILHA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Correo) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Correo y contraseña son obligatorios.");

            var usuario = await _context.Usuarios
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            // Mensaje genérico a propósito: no revelar si fue el correo
            // o la contraseña lo que falló (evita que alguien adivine correos válidos)
            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos.");

            if (usuario.Estado == ProyectoFILHAAPI.Entidades.Enums.EstadoEnum.Inactivo)
                return Unauthorized("Esta cuenta está inactiva.");

            bool passwordValida = BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash);

            if (!passwordValida)
                return Unauthorized("Correo o contraseña incorrectos.");

            var response = new LoginResponseDto
            {
                UsuarioId = usuario.Id,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                ClienteId = usuario.ClienteId,
                NombreCliente = usuario.Cliente?.Nombre
            };

          

            return Ok(response);
        }

        // POST: api/auth/registro
        [HttpPost("registro")]
        public async Task<ActionResult<LoginResponseDto>> Registro(RegistroRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Correo) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Correo y contraseña son obligatorios.");

            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest("El nombre es obligatorio.");

            if (request.Password.Length < 6)
                return BadRequest("La contraseña debe tener al menos 6 caracteres.");

            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == request.Correo);

            if (correoExiste)
                return BadRequest("Ya existe una cuenta registrada con ese correo.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = new ProyectoFILHAPI.Entidades.Cliente
                {
                    Nombre = request.Nombre,
                    Correo = request.Correo,
                    FechaNacimiento = request.FechaNacimiento,
                    Genero = request.Genero,
                    Telefono = request.Telefono,
                    FechaCreacion = DateTime.Now,
                    Estado = ProyectoFILHAAPI.Entidades.Enums.EstadoEnum.Activo
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync(); // necesitamos el ID generado de Cliente

                var usuario = new ProyectoFILHAPI.Entidades.Usuario
                {
                    Correo = request.Correo,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Rol = "Cliente",
                    ClienteId = cliente.Id,
                    FechaCreacion = DateTime.Now,
                    Estado = ProyectoFILHAAPI.Entidades.Enums.EstadoEnum.Activo
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                var response = new LoginResponseDto
                {
                    UsuarioId = usuario.Id,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol,
                    ClienteId = cliente.Id,
                    NombreCliente = cliente.Nombre
                };

                return CreatedAtAction(nameof(Login), response);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "Ocurrió un error al crear la cuenta. Intenta de nuevo.");
            }
        }
    }
}