/*
    * Nombre: SendEmailPokemonDto.cs
    * Descripción: modelo utilizado para la estructura base del objeto proveniente del body para exportar el detalle de Pokémon. 
*/

namespace PokemonServer.DTOs;

public class SendPokemonListEmailDto
{
    public string Recipient { get; set; } = string.Empty;

    public List<PokemonSummaryDto> Pokemons { get; set; } = [];
}

public class SendPokemonDetailEmailDto
{
    public string Recipient { get; set; } = string.Empty;
}