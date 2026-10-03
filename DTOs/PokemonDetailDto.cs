/*
    * Nombre: PokemonDetailDto.cs
    * Descripción: objeto de transferencia de datos utilizado para el detalle del Pokémon. 
*/

namespace PokemonServer.DTOs;

public class PokemonDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    public List<string> Types { get; set; } = [];
}