using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.API.Data;
using ProyectoFILHAAPI.Entidades;
using ProyectoFILHAAPI.Entidades.ProyectoFILHA.API.Models.Entidades;

namespace ProyectoFILHA.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CosmeticosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CosmeticosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cosmeticos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cosmetico>>> Get()
        {
            return await _context.Cosmeticos
                .Include(c => c.Categoria)
                .Include(c => c.Presentacion)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        // GET: api/cosmeticos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cosmetico>> Get(int id)
        {
            var cosmetico = await _context.Cosmeticos
                .Include(c => c.Categoria)
                .Include(c => c.Presentacion)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cosmetico == null)
                return NotFound();

            return cosmetico;
        }

        // POST: api/cosmeticos
        [HttpPost]
        public async Task<ActionResult<Cosmetico>> Post(Cosmetico cosmetico)
        {
            // Validar categoría
            bool categoriaValida = await _context.Categorias
                .AnyAsync(c =>
                    c.Id == cosmetico.CategoriaId);

            if (!categoriaValida)
                return BadRequest("Categoría inválida.");

            // Validar presentación
            bool presentacionValida = await _context.Presentaciones
                .AnyAsync(p =>
                    p.Id == cosmetico.PresentacionId);

            if (!presentacionValida)
                return BadRequest("Presentación inválida.");

            cosmetico.FechaCreacion = DateTime.Now;

            _context.Cosmeticos.Add(cosmetico);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = cosmetico.Id },
                cosmetico);
        }

        // PUT: api/cosmeticos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            int id,
            Cosmetico cosmetico)
        {
            if (id != cosmetico.Id)
                return BadRequest();

            var cosmeticoDb =
                await _context.Cosmeticos.FindAsync(id);

            if (cosmeticoDb == null)
                return NotFound();

            bool categoriaValida = await _context.Categorias
                .AnyAsync(c =>
                    c.Id == cosmetico.CategoriaId);

            if (!categoriaValida)
                return BadRequest("Categoría inválida.");

            bool presentacionValida = await _context.Presentaciones
                .AnyAsync(p =>
                    p.Id == cosmetico.PresentacionId);

            if (!presentacionValida)
                return BadRequest("Presentación inválida.");

            cosmeticoDb.Nombre = cosmetico.Nombre;
            cosmeticoDb.Precio = cosmetico.Precio;
            cosmeticoDb.CantDisponible = cosmetico.CantDisponible;
            cosmeticoDb.Estado = cosmetico.Estado;
            cosmeticoDb.CategoriaId = cosmetico.CategoriaId;
            cosmeticoDb.PresentacionId = cosmetico.PresentacionId;
            cosmeticoDb.Descripcion = cosmetico.Descripcion;
            cosmeticoDb.EsVegano = cosmetico.EsVegano;
            cosmeticoDb.EsDermatologico = cosmetico.EsDermatologico;

            // NO modificar FechaCreacion

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cosmeticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cosmetico =
                await _context.Cosmeticos.FindAsync(id);

            if (cosmetico == null)
                return NotFound();

            _context.Cosmeticos.Remove(cosmetico);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}