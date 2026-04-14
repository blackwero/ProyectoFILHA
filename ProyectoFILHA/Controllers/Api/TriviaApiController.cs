using Microsoft.AspNetCore.Mvc;
using ProyectoFILHA.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class TriviaApiController : ControllerBase
{
    private readonly ITriviaService _triviaService;

    public TriviaApiController(ITriviaService triviaService)
    {
        _triviaService = triviaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPreguntas()
    {
        var data = await _triviaService.ObtenerPreguntas();
        return Ok(data);
    }
}