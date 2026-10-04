/*
 * Nombre: PokemonSpeciesDetailResponse.cs
 * Descripción: modelo de respuesta de la petición para obtener el detalle de una especie de pokémon.
 */

namespace PokemonServer.Models;

public class PokemonSpeciesDetailResponse
{
    public int Id { get; set; }

    public List<PokemonGenusItem> Genera { get; set; } = [];
}

public class PokemonGenusItem
{
    public string Genus { get; set; } = string.Empty;

    public PokemonLanguage Language { get; set; } = new();
}

public class PokemonLanguage
{
    public string Name { get; set; } = string.Empty;
}