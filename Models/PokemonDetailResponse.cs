/*
    * Nombre: PokemonDetailResponse.cs
    * Descripción: modelo de respuesta de la petición para obtener el detalle de los Pokémons.
*/

using System.Text.Json.Serialization;

namespace PokemonServer.Models;

public class PokemonDetailResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    public SpritesResponse Sprites { get; set; } = new();
    public List<TypeResponse> Types { get; set; } = [];
}

public class SpritesResponse
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
}

public class TypeResponse
{
    public int Slot { get; set; }
    public TypeInfoResponse Type { get; set; } = new();
}

public class TypeInfoResponse
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}