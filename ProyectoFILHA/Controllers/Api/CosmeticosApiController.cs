
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;

namespace ProyectoFILHA.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosmeticosApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CosmeticosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cosmeticos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cosmetico>>> GetCosmeticos()
        {
            return await _context.Cosmeticos.ToListAsync();
        }

        // GET: api/cosmeticos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cosmetico>> GetCosmetico(int id)
        {
            var cosmetico = await _context.Cosmeticos.FindAsync(id);

            if (cosmetico == null)
            {
                return NotFound();
            }

            return cosmetico;
        }

        // POST: api/cosmeticos
        [HttpPost]
        public async Task<ActionResult<Cosmetico>> CreateCosmetico(Cosmetico cosmetico)
        {
            _context.Cosmeticos.Add(cosmetico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCosmetico), new { id = cosmetico.Id }, cosmetico);
        }


        // PUT: api/cosmeticos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCosmetico(int id, Cosmetico cosmetico)
        {
            if (id != cosmetico.Id)
            {
                return BadRequest();
            }

            _context.Entry(cosmetico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cosmeticos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    }

}