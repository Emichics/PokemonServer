/*
    * Nombre: PokemonListResponse.cs
    * Descripción: modelo de respuesta de la petición para obtener el listado de pokémon. 
*/

namespace PokemonServer.Models;

public class PokemonListResponse
{
    public int Count { get; set; }
    public List<PokemonListItem> Results { get; set; } = [];
}

public class PokemonListItem
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}