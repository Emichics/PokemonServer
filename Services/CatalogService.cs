/*
    * Nombre: CatalogService.cs
    * Descripción: clase que se encarga de los servicios referentes al Catálogo.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el método para obtener el catálogo de Tipos de Pokémon GetPokemonTypesAsync() 
*/

using PokemonServer.Clients;
using PokemonServer.DTOs;
using PokemonServer.Utils;
using PokemonServer.Exceptions;

namespace PokemonServer.Services;

public class CatalogService
{
    private readonly PokemonApiClient _pokemonApiClient;

    public CatalogService(PokemonApiClient pokemonApiClient)
    {
        _pokemonApiClient = pokemonApiClient;
    }

    public async Task<List<PokemonTypeDto>> GetPokemonTypesAsync()
    {
        try
        {
            var response = await _pokemonApiClient.GetPokemonTypesAsync();

            return response.Results
                .Select(x => new PokemonTypeDto
                {
                    Id = StringUtils.GetIdFromUrl(x.Url),
                    Name = x.Name
                })
                .ToList();
        }
        catch (CustomException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new DefaultException();
        }
    }

}