/*
    * Nombre: PokemonListDto.cs
    * Descripción: objeto de transferencia de datos utilizado para la información general del pokémon. 
*/

namespace PokemonServer.DTOs;

public class PokemonListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}