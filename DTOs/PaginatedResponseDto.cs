/*
    * Nombre: PaginatedResponseDto.cs
    * Descripción: objeto de transferencia de datos genérico utilizado para las consultas paginadas. 
*/

namespace PokemonServer.DTOs;

public class PaginatedResponseDto<T>
{
    public List<T> Items { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}