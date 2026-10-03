/*
    * Nombre: PokemonSummaryDto.cs
    * Descripción: objeto de transferencia de datos utilizado para la información general del pokémon mostrada en el Grid. 
*/

namespace PokemonServer.DTOs;

public class PokemonSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}