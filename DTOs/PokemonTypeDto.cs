/*
    * Nombre: PokemonTypeDto.cs
    * Descripción: objeto de transferencia de datos utilizado para los tipos de pokémon. 
*/

namespace PokemonServer.DTOs;

public class PokemonTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}