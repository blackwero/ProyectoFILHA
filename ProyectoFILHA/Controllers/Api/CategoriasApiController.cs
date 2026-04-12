using Microsoft.AspNetCore.Mvc;

namespace ProyectoFILHA.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasApiController : ControllerBase
    {
        private static List<string> categorias = new List<string>
        {
            "Maquillaje",
            "Cuidado de la piel",
            "Fragancias",
            "Cabello"
        };

        [HttpGet]
        public IActionResult GetCategorias()
        {
            return Ok(categorias);
        }
    }
}