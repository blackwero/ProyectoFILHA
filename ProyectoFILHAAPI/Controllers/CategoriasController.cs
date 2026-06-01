using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.API.Data;
using ProyectoFILHAAPI.Entidades.ProyectoFILHA.API.Models.Entidades;

namespace ProyectoFILHA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            return await _context.Categorias
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return categoria;
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = categoria.Id },
                categoria);
        }

        // PUT: api/categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            int id,
            Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest("El Id de la URL no coincide con el Id enviado.");

            var categoriaDb = await _context.Categorias.FindAsync(id);

            if (categoriaDb == null)
                return NotFound();

            // Actualizar únicamente los campos permitidos
            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Estado = categoria.Estado;

            // NO modificar FechaCreacion

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}