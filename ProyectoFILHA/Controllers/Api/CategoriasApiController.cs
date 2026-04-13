

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoFILHA.Controllers.Api
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return categoria;
        }

        // POST: api/categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            categoria.Estado = EstadoEnum.Activo; // 👈 importante

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }

        // PUT: api/categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
                return BadRequest();

            var categoriaDb = await _context.Categorias.FindAsync(id);

            if (categoriaDb == null)
                return NotFound();

            // Actualizamos solo lo necesario
            categoriaDb.Nombre = categoria.Nombre;
            categoriaDb.Estado = categoria.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}