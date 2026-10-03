/*
    * Nombre: CustomException.cs
    * Descripción: clase manejadora de errores personalizados.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el error personalizado "DefaultException".
*/

using PokemonServer.Constants;

namespace PokemonServer.Exceptions;

public class CustomException : Exception
{
    public int Status { get; }
    public string Message { get; }

    protected CustomException(Messages.MessageConstructor message)
        : base(message.Message)
    {
        Status = message.Status;
        Message = message.Message;
    }
}

public class DefaultException : CustomException
{
    public DefaultException()
        : base(Messages.Error.Default)
    {
    }
}
