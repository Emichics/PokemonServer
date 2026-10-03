/*
    * Nombre: ExportPokemonListDto.cs
    * Descripción: modelo utilizado para la estructura base del objeto proveniente del body para exportar listado de Pokémon. 
*/

namespace PokemonServer.DTOs;

public class ExportPokemonListDto
{
    public List<PokemonSummaryDto> Pokemons { get; set; } = [];
}