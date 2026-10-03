/*
    * Nombre: CustomException.cs
    * Descripción: clase manejadora de errores personalizados.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el error personalizado "DefaultException".
        - Se agrega el error personalizado "PokemonApiException".
*/

using PokemonServer.Constants;

namespace PokemonServer.Exceptions;

public class CustomException : Exception
{
    public int? Status { get; }
    public string? Message { get; }

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

public class PokemonApiException : CustomException
{
    public PokemonApiException()
        : base(Messages.Error.PokemonApi)
    {
    }
}
