/*
    * Nombre: Messages.cs
    * Descripción: clase que almacena mensajes de respuesta.
    * Historial de cambios: 
        02/10/2026
        - Se agregan los mensajes de respuesta Ok y Default. 
*/

namespace PokemonServer.Constants;

public static class Messages
{
    public class MessageConstructor
    {
        public int Status { get; init; }
        public string Message { get; init; } = string.Empty;
    }

    public static class Success
    {
        public static readonly MessageConstructor Ok = new()
        {
            Status = 1,
            Message = "Operación realizada correctamente."
        };
    }

    public static class Error
    {
        public static readonly MessageConstructor Default = new()
        {
            Status = 0,
            Message = "Ocurrió un error."
        };
    }
}

