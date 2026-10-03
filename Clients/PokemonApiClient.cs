/*
    * Nombre: PokemonApiClient.cs
    * Descripción: cliente del API de Pokémon.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el método para obtener el catálogo de Tipos de Pokémon GetPokemonTypesAsync()
        - Se agrega el método para obtener el listado completo de todos los pokémon GetPokemonListAsync()
        - Se agrega el método para obtener la información de Pokémon por tipo GetPokemonTypeAsync()
        
        03/10/2026
        - Se agrega el método para obtener el detalle de un Pokémon por medio de su id GetPokemonById()
*/

using System.Net.Http;
using PokemonServer.Exceptions;
using PokemonServer.Constants;
using PokemonServer.Models;
using PokemonServer.DTOs;


namespace PokemonServer.Clients;

public class PokemonApiClient 
{
    private readonly HttpClient _httpClient;

    public PokemonApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonTypeResponse> GetPokemonTypesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("type");

            response.EnsureSuccessStatusCode();

            var respPokemonTypes = await response.Content
                .ReadFromJsonAsync<PokemonTypeResponse>();

            return respPokemonTypes ?? throw new PokemonApiException();
        }
        catch (CustomException)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            throw new PokemonApiException();
        }
        catch (Exception)
        {
            throw new DefaultException();
        }
    }

    public async Task<PokemonListResponse> GetPokemonListAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"pokemon?limit={AppConstants.PaginationValues.MaxPageSize}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<PokemonListResponse>();

            return result ?? throw new PokemonApiException();
        }
        catch (CustomException)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            throw new PokemonApiException();
        }
        catch (Exception)
        {
            throw new DefaultException();
        }
    }

    public async Task<PokemonTypeDetailResponse> GetPokemonTypeAsync(string type)
    {
        try
        {
            var response = await _httpClient.GetAsync($"type/{type}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<PokemonTypeDetailResponse>();

            return result ?? throw new PokemonApiException();
        }
        catch (CustomException)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            throw new PokemonApiException();
        }
        catch (Exception)
        {
            throw new DefaultException();
        }
    }
    
    public async Task<PokemonDetailDto> GetPokemonByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"pokemon/{id}");

            response.EnsureSuccessStatusCode();

            var pokemon = await response.Content.ReadFromJsonAsync<PokemonDetailResponse>();

            if (pokemon is null) throw new PokemonApiException();

            return new PokemonDetailDto
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                Image = pokemon.Sprites.FrontDefault ?? string.Empty,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                Types = pokemon.Types.Select(x => x.Type.Name).ToList()
            };
        }
        catch (CustomException)
        {
            throw;
        }
        catch (HttpRequestException)
        {
            throw new PokemonApiException();
        }
        catch (Exception)
        {
            throw new DefaultException();
        }
    }

}