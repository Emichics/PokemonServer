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
        public const int DefaultPageSize = 10;
    }

    public static class ExcelValues
    {
        public const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string PokemonFileName = "pokemons.xlsx";
        public const string PokemonSummaryWorkSheetName = "Pokemons";
        public const string PokemonDetailWorkSheetName = "Pokemon";

        public static class PokemonSummaryFields
        {
            public const string Id = "Id";
            public const string Name = "Nombre";
            public const string Image = "Imagen";
        }

        public static class PokemonDetailFields
        {
            public const string Field = "Campo";
            public const string Value = "Valor";
            public const string Id = "Id";
            public const string Name = "Nombre";
            public const string Height = "Altura";
            public const string Weight = "Peso";
        }
    }

    public static class EmailValues
    {
        public static class PokemonList
        {
            public const string Subject = "Listado de Pokémon";
            public const string Message = "Se adjunta el Excel.";
        }
        
        public static class PokemonDetail
        {
            public const string Subject = "Información del Pokémon";
            public const string Message = "Se adjunta el Excel.";
        }
    }

}