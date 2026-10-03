/*
    * Nombre: PokemonTypeDetailResponse.cs
    * Descripción: modelo de respuesta de la petición para obtener el listado tipos de pokémon junto con los pokémones que pertenecen. 
*/

namespace PokemonServer.Models;

public class PokemonTypeDetailResponse
{
    public string Name { get; set; } = string.Empty;
    public List<PokemonTypeDetailItem> Pokemon { get; set; } = [];
}

public class PokemonTypeDetailItem
{
    public PokemonTypePokemon Pokemon { get; set; } = new();
}

public class PokemonTypePokemon
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}