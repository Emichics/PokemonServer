/*
    * Nombre: AppConstants.cs
    * Descripción: clase para almacenar constantes de la aplicación. 
*/

namespace PokemonServer.Constants;

public static class AppConstants
{
    public static class CacheKeys
    {
        public const string PokemonList = "PokemonList";
        public const string TypesList = "TypesList";
        public const string PokemonDetailList = "PokemonDetailList";
    }

    public static class PaginationValues
    {
        public const int MaxPageSize = 10000;
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 20;
    }
}