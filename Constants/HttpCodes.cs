/*
    * Nombre: HttpCodes.cs
    * Descripción: clase que almacena los códigos HTTP.
    * Historial de cambios: 
        02/10/2026
        - Se agregan los códigos Ok, BadRequest e InternalServerError. 
*/

namespace PokemonServer.Constants;

public static class HttpCodes
{
    public const int Ok = 200;
    public const int BadRequest = 400;
    public const int InternalServerError = 500;
}