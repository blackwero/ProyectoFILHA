using Microsoft.AspNetCore.Mvc;
using ProyectoFILHA.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class DragonBallApiController : ControllerBase
{
    private readonly IDragonBallService _service;

    public DragonBallApiController(IDragonBallService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersonajes()
    {
        var data = await _service.ObtenerPersonajes();
        return Ok(data);
    }
}