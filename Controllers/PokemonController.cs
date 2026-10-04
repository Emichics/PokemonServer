/*
    * Nombre: PokemonController.cs
    * Descripción: controlador de la entidad pokémon.
    * Historial de cambios: 
        02/10/2026
        - Se agrega la ruta para obtener el listado de Pokémon GetPokemonList()
        
        03/10/2026
        - Se agrega la ruta para obtener el detalle de un Pokémon por medio de su Id GetPokemonById()
        - Se agrega la ruta para exportar archivo de Excel con los pokémon brindados.
        - Se agrega la ruta para enviar por correo electrónico un archivo Excel con los pokémon brindados SendEmail(body{recipient, pokemons}).
        - Se agrega la ruta para enviar por correo electrónico un archivo Excel con el detalle del Pokémon SendEmail(id, body{recipient).      
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
    public async Task<ActionResult> GetPokemonList(int page = AppConstants.PaginationValues.DefaultPage, int pageSize = AppConstants.PaginationValues.DefaultPageSize, string? name = null, string? genus = null)
    {
        try
        {
            page = page < 1 ? AppConstants.PaginationValues.DefaultPage : page;
            pageSize = pageSize < 1 ? AppConstants.PaginationValues.DefaultPageSize : pageSize;
            name = string.IsNullOrWhiteSpace(name) ? null : name.Trim();
            genus = string.IsNullOrWhiteSpace(genus) ? null : genus.Trim();

            var result = await _pokemonService.GetPokemonsAsync(page, pageSize, name, genus);
            var response = new ApiResponseDto<PaginatedResponseDto<PokemonSummaryDto>>
            {
                Status = Messages.Success.Ok.Status,
                Message = Messages.Success.Ok.Message,
                Data = result
            };

            return StatusCode(HttpCodes.Ok, response);
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

    [HttpGet("{id}")]
    public async Task<ActionResult> GetPokemonById(int id)
    {
        try
        {
            var result = await _pokemonService.GetPokemonByIdAsync(id);
            var response = new ApiResponseDto<PokemonDetailDto>
            {
                Status = Messages.Success.Ok.Status,
                Message = Messages.Success.Ok.Message,
                Data = result
            };

            return StatusCode(HttpCodes.Ok, response);
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

    [HttpPost("export")]
    public IActionResult ExportPokemonList([FromBody] ExportPokemonListDto request)
    {
        try
        {
            var excel = _pokemonService.ExportPokemonList(request.Pokemons);

            return File(
                excel,
                AppConstants.ExcelValues.ContentType,
                AppConstants.ExcelValues.PokemonFileName
            );
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

    [HttpPost("sendemail")]
    public async Task<IActionResult> SendEmail([FromBody] SendPokemonListEmailDto request)
    
    {
        try
        {
            await _pokemonService.SendEmailPokemonListAsync(request.Recipient, request.Pokemons);
            return StatusCode(HttpCodes.Ok, Messages.Success.Ok);
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

    [HttpPost("{id}/sendemail")]
    public async Task<IActionResult> SendEmail(int id, [FromBody] SendPokemonDetailEmailDto request)
    {
        try
        {
            await _pokemonService.SendEmailPokemonDetailAsync(request.Recipient, id );
            return StatusCode(HttpCodes.Ok, Messages.Success.Ok);
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
