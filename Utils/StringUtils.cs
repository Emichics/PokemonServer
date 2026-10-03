/*
    * Nombre: StringUtils.cs
    * Descripción: utilería para datos de tipos String.
    * Cambios: 
        02/10/2026 - Creación del método GetIdFromUrl() para extraer el id de la url.
*/

using PokemonServer.Exceptions;

namespace PokemonServer.Utils;

public static class StringUtils
{
    public static int GetIdFromUrl(string url)
    {
        try 
        {
            var parts = url.TrimEnd('/').Split('/');

            return int.Parse(parts[^1]);
        }
        catch(Exception)
        {
            throw new DefaultException();
        }
        
    }
}