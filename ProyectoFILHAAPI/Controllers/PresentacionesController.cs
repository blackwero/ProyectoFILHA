using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.API.Data;
using ProyectoFILHAAPI.Entidades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoFILHAAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentacionesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PresentacionesController(
            AppDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presentacion>>> Get()
        {
            return await _context.Presentaciones
                .ToListAsync();
        }

        // GET ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Presentacion>> Get(int id)
        {
            var presentacion =
                await _context.Presentaciones.FindAsync(id);

            if (presentacion == null)
                return NotFound();

            return presentacion;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult> Post(
            Presentacion presentacion)
        {
            presentacion.FechaCreacion = DateTime.Now;

            _context.Presentaciones.Add(presentacion);

            await _context.SaveChangesAsync();

            return Ok(presentacion);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(
            int id,
            Presentacion presentacion)
        {
            if (id != presentacion.Id)
                return BadRequest();

            _context.Entry(presentacion)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var presentacion =
                await _context.Presentaciones.FindAsync(id);

            if (presentacion == null)
                return NotFound();

            _context.Presentaciones.Remove(presentacion);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
