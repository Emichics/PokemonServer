/*
    * Nombre: PokemonController.cs
    * Descripción: controlador de la entidad pokémon.
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
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponseDto<PokemonListDto>>> GetPokemonList(int page = AppConstants.PaginationValues.DefaultPage, int pageSize = AppConstants.PaginationValues.DefaultPageSize, string? name = null, string? type = null)
    {
        try
        {
            page = page < 1 ? AppConstants.PaginationValues.DefaultPage : page;
            pageSize = pageSize < 1 ? AppConstants.PaginationValues.DefaultPageSize : pageSize;
            name = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            type = string.IsNullOrWhiteSpace(type) ? null : type.Trim().ToLower();

            var result = await _pokemonService.GetPokemonsAsync(page, pageSize, name, type);

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
