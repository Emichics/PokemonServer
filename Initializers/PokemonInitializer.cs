/*
    * Nombre: PokemonInitializer.cs
    * Descripción: "Hay que pensar jeje"
    * Historial: 
        02/10/2026 
        - Creación del método InitializeAsync() para inicializar los caché.
        - Creación del método LoadPokemonListAsync() para guardar en el caché la información básica de todos los pokémon. 
        - Creación del método LoadPokemonTypesAsync() para guardar en el caché la información de todos los tipos de pokémon.
        - Creación del método InitializePokemonDetailList() para incializar el caché de la información del detalle de los Pokémon.
*/

using Microsoft.Extensions.Caching.Memory;
using PokemonServer.Clients;
using PokemonServer.Utils;
using PokemonServer.Constants;
using PokemonServer.DTOs;

namespace PokemonServer.Initializers;

public class PokemonInitializer
{
    private readonly PokemonApiClient _pokemonApiClient;
    private readonly IMemoryCache _memoryCache;

    public PokemonInitializer(
        PokemonApiClient pokemonApiClient,
        IMemoryCache memoryCache)
    {
        _pokemonApiClient = pokemonApiClient;
        _memoryCache = memoryCache;
    }

    public async Task InitializeAsync()
    {
        await LoadPokemonListAsync();
        await LoadPokemonTypesAsync();
        InitializePokemonDetailList();
    }

    private async Task LoadPokemonListAsync()
    {
        try{
            var response = await _pokemonApiClient.GetPokemonListAsync();

            var pokemonList = response.Results
                .Select(x => new PokemonListDto
                {
                    Id = StringUtils.GetIdFromUrl(x.Url),
                    Name = x.Name,
                    Url = x.Url
                })
                .ToDictionary(x => x.Id);

            _memoryCache.Set(AppConstants.CacheKeys.PokemonList, pokemonList);
        }
        catch(Exception)
        {
            Console.WriteLine($"Error de incialización de caché: {AppConstants.CacheKeys.PokemonList}");
        }
        
    }

    private async Task LoadPokemonTypesAsync()
    {
        try
        {
            var response = await _pokemonApiClient.GetPokemonTypesAsync();

            var typeCache = new Dictionary<string, List<int>>();

            foreach (var type in response.Results)
            {
                var typeDetail = await _pokemonApiClient
                    .GetPokemonTypeAsync(type.Name);

                var pokemonIds = typeDetail.Pokemon
                    .Select(x => StringUtils.GetIdFromUrl(x.Pokemon.Url))
                    .ToList();

                typeCache[type.Name] = pokemonIds;
            }

            _memoryCache.Set(AppConstants.CacheKeys.TypesList, typeCache);
        }
        catch(Exception)
        {
            Console.WriteLine($"Error de incialización de caché: {AppConstants.CacheKeys.TypesList}");
        }
        
    }

    private void InitializePokemonDetailList()
    {
        try
        {
            var detailList = new Dictionary<int, PokemonDetailDto>();
            _memoryCache.Set(AppConstants.CacheKeys.PokemonDetailList, detailList);
        }
        catch(Exception)
        {
            Console.WriteLine($"Error de incialización de caché: {AppConstants.CacheKeys.PokemonDetailList}");
        }
        
    }
}