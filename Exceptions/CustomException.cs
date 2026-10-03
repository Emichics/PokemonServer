/*
    * Nombre: CustomException.cs
    * Descripción: clase manejadora de errores personalizados.
    * Historial de cambios: 
        02/10/2026
        - Se agrega el error personalizado "DefaultException".
        - Se agrega el error personalizado "PokemonApiException".
        - Se agrega el error personalizado "ExcelFileException".
        - Se agrega el error personalizado "SendEmailException".
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

public class ExcelFileException : CustomException
{
    public ExcelFileException()
        : base(Messages.Error.ExcelFile)
    {
    }
}

public class SendEmailException : CustomException
{
    public SendEmailException()
        : base(Messages.Error.ExcelFile)
    {
    }
}