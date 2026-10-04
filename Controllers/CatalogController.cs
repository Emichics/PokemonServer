/*
    * Nombre: CatalogController.cs
    * Descripción: controlador de los catálogos.
    * Historial de cambios: 
        02/10/2026
        - Se agrega la ruta para obtener el catálogo de Genus de Pokémon GetTypes() 
*/

using Microsoft.AspNetCore.Mvc;
using PokemonServer.Services;
using PokemonServer.DTOs;
using PokemonServer.Constants;
using PokemonServer.Exceptions;

namespace PokemonServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly CatalogService _catalogService;

    public CatalogController(CatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("genus")]
    public async Task<IActionResult> GetGenera()
    {
        try
        {
            var response = await _catalogService.GetPokemonGeneraAsync();

            var result = new ApiResponseDto<List<PokemonGenusDto>>
            {
                Status = Messages.Success.Ok.Status,
                Message = Messages.Success.Ok.Message,
                Data = response
            };

            return StatusCode(HttpCodes.Ok, result);
        }
        catch (Exception ex)
        {
            var customException = ex as CustomException;
            var result = new ApiResponseDto<object>
            {
                Status = customException.Status ?? Messages.Error.Default.Status,
                Message = customException.Message ?? Messages.Error.Default.Message
            };

            return StatusCode(HttpCodes.InternalServerError, result);
        }
    }
}