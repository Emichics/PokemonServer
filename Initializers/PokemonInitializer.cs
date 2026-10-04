/*
    * Nombre: PokemonInitializer.cs
    * Descripción: clase contenedora de las cargas iniciales del aplicativo.
    * Historial: 
        02/10/2026 
        - Creación del método InitializeAsync() para inicializar los caché.
        - Creación del método LoadPokemonListAsync() para guardar en el caché la información básica de todos los pokémon. 
        - Creación del método LoadPokemonGeneraAsync() para guardar en el caché la información de todos las especies de pokémon.
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
        await LoadPokemonGeneraAsync();
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

    private async Task LoadPokemonGeneraAsync()
    {
        try
        {
            var response = await _pokemonApiClient.GetPokemonSpeciesAsync();
            var genusCache = new Dictionary<string, HashSet<int>>();

            for (int i = 0; i < response.Results.Count; i += AppConstants.BatchSize)
            {
                var batch = response.Results
                    .Skip(i)
                    .Take(AppConstants.BatchSize)
                    .ToList();

                var tasks = batch.Select(species =>
                    _pokemonApiClient.GetPokemonSpeciesDetailAsync(species.Url)
                );

                var speciesDetails = await Task.WhenAll(tasks);

                foreach (var speciesDetail in speciesDetails)
                {
                    var genus = speciesDetail.Genera
                        .FirstOrDefault(x => x.Language.Name == "en")
                        ?.Genus;

                    if (string.IsNullOrWhiteSpace(genus)) continue;

                    if (!genusCache.ContainsKey(genus)) genusCache[genus] = [];

                    genusCache[genus].Add(speciesDetail.Id);
                }
            }

            _memoryCache.Set(
                AppConstants.CacheKeys.GeneraList,
                genusCache
            );
        }
        catch (Exception)
        {
            Console.WriteLine(
                $"Error de inicialización de caché: {AppConstants.CacheKeys.GeneraList}"
            );
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