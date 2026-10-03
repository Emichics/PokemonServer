/*
    * Nombre: PokemonService.cs
    * Descripción: clase encargada de los servicios referentes a la entidad Pokémon
    * Historial de cambios: 
        02/10/2026 
            - Creación del método GetPokemonsAsync() para obtener el listado de pokemones.
            - Implementación de caché al realizar consultas de pokemones. 
        
        03/10/2026
            -Creación del método GetPokemonByIdAsync() para obtener el detalle de un poḱémon por medio de su Id.
            -Creación del método ExportPokemonList() para crear archivo Excel con el listado de Pokémon brindado.
            -Creación del método ExportPokemonDetail() para crear archivo Excel con el detalle del Pokémon solicitado.
            -Creación del método SendEmailPokemonListAsync() para enviar por correo un archivo Excel con el listado de Pokémon brindado.
            -Creación del método SendEmailPokemonDetailAsync() para para enviar por correo un archivo Excel con el detalle del Pokémon solicitado.
*/

using PokemonServer.Clients;
using PokemonServer.DTOs;
using PokemonServer.Exceptions;
using PokemonServer.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace PokemonServer.Services;

public class PokemonService
{
    private readonly IMemoryCache _memoryCache;
    private readonly PokemonApiClient _pokemonApiClient;
    private readonly ExcelService _excelService;
    private readonly EmailService _emailService;

    public PokemonService(
        IMemoryCache memoryCache,
        PokemonApiClient pokemonApiClient,
        ExcelService excelService,
        EmailService emailService)
    {
        _memoryCache = memoryCache;
        _pokemonApiClient = pokemonApiClient;
        _excelService = excelService;
        _emailService = emailService;
    }

    public async Task<PaginatedResponseDto<PokemonSummaryDto>> GetPokemonsAsync(int page, int pageSize, string? name, string? type)
    {
        try
        {
            //Obtiene la lista general completa de pokemons en el caché
            var pokemonList =
                _memoryCache.Get<Dictionary<int, PokemonListDto>>(
                    AppConstants.CacheKeys.PokemonList
                ) ?? [];
            
            //Obtiene la lista completa de los tipos de pokémon que existen en el caché
            var typesList =
                _memoryCache.Get<Dictionary<string, List<int>>>(
                    AppConstants.CacheKeys.TypesList
                ) ?? [];
            
            //Obtiene la lista de detalles de todos los pokémons que estén en el caché
            var pokemonDetailList =
                _memoryCache.Get<Dictionary<int, PokemonDetailDto>>(
                    AppConstants.CacheKeys.PokemonDetailList
                ) ?? [];

            IEnumerable<int> pokemonIds = pokemonList.Keys;

            //Si se filtra por "type" se obtienen todos los ids de los pokemons con ese "type"
            if (type is not null)
            {
                var typeExists = typesList.TryGetValue(
                    type,
                    out var typePokemonIds
                );

                pokemonIds = typeExists ? pokemonIds.Intersect(typePokemonIds) : []; 

            }

            //Si se filtra por "name" se obtienen todos los ids de los pokemons que coincidan con el nombre dado
            if (name is not null)
            {
                pokemonIds = pokemonIds.Where(id =>
                    pokemonList[id].Name.Contains(
                        name,
                        StringComparison.OrdinalIgnoreCase
                    )
                );
            }

            var filteredIds = pokemonIds.ToList();
            
            //Se realizan las operaciones para obtener los datos de la paginación
            var totalItems = filteredIds.Count;
            var totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize
            );
            var pagedIds = filteredIds
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var items = new List<PokemonSummaryDto>();

            //Se valida si los pokémons que se enviarán ya tienen su detalle en el caché. 
            foreach (var id in pagedIds)
            {
                var exists = pokemonDetailList.TryGetValue(id, out var pokemonDetail);

                //En caso de que no, se consulta su detalle y se guarda en el caché.
                if (!exists)
                {
                    pokemonDetail = await _pokemonApiClient.GetPokemonByIdAsync(id);
                    pokemonDetailList[id] = pokemonDetail;
                }

                var pokemonSummary = new PokemonSummaryDto
                {
                    Id = pokemonDetail.Id,
                    Name = pokemonDetail.Name,
                    Image = pokemonDetail.Image ?? string.Empty
                };
                
                items.Add(pokemonSummary);
            }

            return new PaginatedResponseDto<PokemonSummaryDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
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

    public async Task<PokemonDetailDto> GetPokemonByIdAsync(int id)
    {
        try
        {
            var pokemonDetailList =
                _memoryCache.Get<Dictionary<int, PokemonDetailDto>>(
                    AppConstants.CacheKeys.PokemonDetailList
                ) ?? [];
            
            var exists = pokemonDetailList.TryGetValue(id, out var pokemonDetail);
            if (!exists)
            {
                pokemonDetail = await _pokemonApiClient.GetPokemonByIdAsync(id);
                pokemonDetailList[id] = pokemonDetail;
            }

            return pokemonDetail;
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

    public byte[] ExportPokemonList(List<PokemonSummaryDto> pokemons)
    {
        return _excelService.GeneratePokemonExcel(pokemons);
    }

    public async Task<byte[]> ExportPokemonDetail(int id)
    {
        try
        {
            var pokemon = await _pokemonApiClient.GetPokemonByIdAsync(id);
            return _excelService.GeneratePokemonExcel(pokemon);
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

    public async Task SendEmailPokemonListAsync(string recipient, List<PokemonSummaryDto> pokemons)
    {
        try
        {
            var excel = _excelService.GeneratePokemonExcel(pokemons);
            await _emailService.SendEmailAsync(
                recipient,
                AppConstants.EmailValues.PokemonList.Subject,
                AppConstants.EmailValues.PokemonList.Message, 
                excel,
                AppConstants.ExcelValues.PokemonFileName,
                AppConstants.ExcelValues.ContentType
            );
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

    public async Task SendEmailPokemonDetailAsync(string recipient, int id)
    {
        try
        {
            var pokemon = await _pokemonApiClient.GetPokemonByIdAsync(id);
            var excel = _excelService.GeneratePokemonExcel(pokemon);
            await _emailService.SendEmailAsync(
                recipient,
                AppConstants.EmailValues.PokemonDetail.Subject,
                AppConstants.EmailValues.PokemonDetail.Message, 
                excel,
                AppConstants.ExcelValues.PokemonFileName,
                AppConstants.ExcelValues.ContentType
            );
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
