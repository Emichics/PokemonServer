/*
    * Nombre: PokemonTypeResponse.cs
    * Descripción: modelo de respuesta de la petición para obtener los tipos de pokémon. 
*/

namespace PokemonServer.Models;

public class PokemonTypeResponse
{
    public List<PokemonTypeItem> Results { get; set; } = [];
}

public class PokemonTypeItem
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}