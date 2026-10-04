/*
    * Nombre: PokemonSpeciesResponse.cs
    * Descripción: modelo de respuesta de la petición para obtener las especies de pokémon.
*/

namespace PokemonServer.Models;

public class PokemonSpeciesResponse
{
    public List<PokemonSpeciesItem> Results { get; set; } = [];
}

public class PokemonSpeciesItem
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}