/*
    * Nombre: CatalogController.cs
    * Descripción: controlador de los catálogos.
    * Historial de cambios: 
        02/10/2026
        - Se agrega la ruta para obtener el catálogo de Tipos de Pokémon GetTypes() 
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

    [HttpGet("types")]
    public async Task<IActionResult> GetTypes()
    {
        try
        {
            var response = await _catalogService.GetPokemonTypesAsync();

            var result = new ApiResponseDto<List<PokemonTypeDto>>
            {
                Status = Messages.Success.Ok.Status,
                Message = Messages.Success.Ok.Message,
                Data = response
            };

            return StatusCode(HttpCodes.Ok, result);
        }
        catch (CustomException ex)
        {
            var result = new ApiResponseDto<object>
            {
                Status = ex.Status,
                Message = ex.Message
            };

            return StatusCode(HttpCodes.InternalServerError, result);
        }
    }
}