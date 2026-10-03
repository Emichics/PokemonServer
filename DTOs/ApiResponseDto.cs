/*
    * Nombre: ApiResponseDto.cs
    * Descripción: objeto de transferencia de datos utilizado para la construcción de la respuesta del API. 
*/

namespace PokemonServer.DTOs;

public class ApiResponseDto<T>
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}