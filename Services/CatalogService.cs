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
using Microsoft.Extensions.Caching.Memory;
using PokemonServer.Constants;

namespace PokemonServer.Services;

public class CatalogService
{
    private readonly PokemonApiClient _pokemonApiClient;
    private readonly IMemoryCache _memoryCache;

    public CatalogService(
        PokemonApiClient pokemonApiClient,
        IMemoryCache memoryCache)
    {
        _pokemonApiClient = pokemonApiClient;
        _memoryCache = memoryCache;
    }

    public async Task<List<PokemonGenusDto>> GetPokemonGeneraAsync()
    {
        try
        {
            if (!_memoryCache.TryGetValue(
                AppConstants.CacheKeys.GeneraList,
                out Dictionary<string, HashSet<int>>? genusCache))
            {
                throw new DefaultException();
            }

            return genusCache.Keys
                .Select(genus => new PokemonGenusDto
                {
                    Name = genus
                })
                .OrderBy(x => x.Name)
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