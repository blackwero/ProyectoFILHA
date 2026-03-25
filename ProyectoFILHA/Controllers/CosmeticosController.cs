using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFILHA.Models;
using ProyectoFILHA.Models.Entidades;
using ProyectoFILHA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFILHA.Controllers
{
    public class CosmeticosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private async Task CargarCombos()
        {
            ViewBag.Categorias = new SelectList(
                await _context.Categorias
                    .Where(c => c.Estado == EstadoEnum.Activo)
                    .ToListAsync(),
                "Id",
                "Nombre"
            );

            ViewBag.Presentaciones = new SelectList(
                await _context.Presentaciones
                    .Where(p => p.Estado == EstadoEnum.Activo)
                    .ToListAsync(),
                "Id",
                "Nombre"
            );
        }
        public CosmeticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cosmeticoes
        public async Task<IActionResult> AdminIndex(
     string buscar,
     int? categoriaId,
     int? presentacionId,
     decimal? precioMin,
     decimal? precioMax,
     int? esVegano,
     int? esDermatologico,
     EstadoEnum? estado,
     int page = 1)
        {
            int pageSize = 10;

            var query = _context.Cosmeticos
                .Include(c => c.Categoria)
                .Include(c => c.Presentacion)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
                query = query.Where(c => c.Nombre.Contains(buscar));

            if (categoriaId.HasValue)
                query = query.Where(c => c.CategoriaId == categoriaId);

            if (presentacionId.HasValue)
                query = query.Where(c => c.PresentacionId == presentacionId);

            if (precioMin.HasValue)
                query = query.Where(c => c.Precio >= precioMin);

            if (precioMax.HasValue)
                query = query.Where(c => c.Precio <= precioMax);

            if (esVegano.HasValue)
                query = query.Where(c => c.EsVegano == esVegano);

            if (esDermatologico.HasValue)
                query = query.Where(c => c.EsDermatologico == esDermatologico);
            if (estado.HasValue)
            {
                query = query.Where(c => c.Estado == estado);
            }

            int totalItems = await query.CountAsync();

            var lista = await query
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Dropdowns
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            ViewBag.Presentaciones = await _context.Presentaciones.ToListAsync();

            // Filtros
            ViewBag.Buscar = buscar;
            ViewBag.CategoriaId = categoriaId;
            ViewBag.PresentacionId = presentacionId;
            ViewBag.PrecioMin = precioMin;
            ViewBag.PrecioMax = precioMax;
            ViewBag.EsVegano = esVegano;
            ViewBag.EsDermatologico = esDermatologico;
            ViewBag.Estado = estado;

            // Paginación
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);




            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            await CargarCombos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cosmetico cosmetico)
        {

            if (ModelState.IsValid)
            {
                cosmetico.FechaCreacion = DateTime.Now;
                _context.Add(cosmetico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminIndex));
            }

            bool categoriaValida = await _context.Categorias
                .AnyAsync(c => c.Id == cosmetico.CategoriaId && c.Estado == EstadoEnum.Activo);

            bool presentacionValida = await _context.Presentaciones
                .AnyAsync(p => p.Id == cosmetico.PresentacionId && p.Estado == EstadoEnum.Activo);

            if (!categoriaValida)
                ModelState.AddModelError("CategoriaId", "Categoría no válida");

            if (!presentacionValida)
                ModelState.AddModelError("PresentacionId", "Presentación no válida");
            await CargarCombos();

            return View(cosmetico);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cosmetico = await _context.Cosmeticos.FindAsync(id);
            if (cosmetico == null)
                return NotFound();

            await CargarCombos();

            return View(cosmetico);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cosmetico cosmetico)
        {
            if (id != cosmetico.Id)
                return NotFound();

            var cosmeticoDb = await _context.Cosmeticos.FindAsync(id);
            if (cosmeticoDb == null)
                return NotFound();

            // 🔒 Validar relaciones activas
            bool categoriaValida = await _context.Categorias
                .AnyAsync(c => c.Id == cosmetico.CategoriaId && c.Estado == EstadoEnum.Activo);

            bool presentacionValida = await _context.Presentaciones
                .AnyAsync(p => p.Id == cosmetico.PresentacionId && p.Estado == EstadoEnum.Activo);

            if (!categoriaValida)
                ModelState.AddModelError("CategoriaId", "La categoría seleccionada no está activa");

            if (!presentacionValida)
                ModelState.AddModelError("PresentacionId", "La presentación seleccionada no está activa");

            if (ModelState.IsValid)
            {
                // 🔥 Actualización controlada
                cosmeticoDb.Nombre = cosmetico.Nombre;
                cosmeticoDb.Precio = cosmetico.Precio;
                cosmeticoDb.CantDisponible = cosmetico.CantDisponible;
                cosmeticoDb.CategoriaId = cosmetico.CategoriaId;
                cosmeticoDb.PresentacionId = cosmetico.PresentacionId;
                cosmeticoDb.Descripcion = cosmetico.Descripcion;
                cosmeticoDb.EsVegano = cosmetico.EsVegano;
                cosmeticoDb.EsDermatologico = cosmetico.EsDermatologico;
                cosmeticoDb.Estado = cosmetico.Estado;

                // ❌ NO tocar FechaCreacion

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(AdminIndex));
            }

            // 🔁 Recargar combos si falla
            await CargarCombos();

            return View(cosmetico);
        }

    }
}
