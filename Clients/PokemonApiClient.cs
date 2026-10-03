/*
    * Nombre: PokemonApiClient.cs
    * Descripción: cliente del API de Pokémon.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el método para obtener el catálogo de Tipos de Pokémon GetPokemonTypesAsync() 
*/

using System.Net.Http;
using PokemonServer.Exceptions;
using PokemonServer.Constants;
using PokemonServer.Models;


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

}