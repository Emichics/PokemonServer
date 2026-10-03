/*
    * Nombre: ExcelService.cs
    * Descripción: manejador de archivos Excel.
    * Historial de cambios: 
        03/10/2026
        - Se agrega el método GeneratePokemonExcel(pokemons) para creación de archivo Excel con un listado de Pokémon. 
        - Se agrega el método GeneratePokemonExcel(pokemon) para creación de archivo Excel con el detalle de un Pokémon.
*/

using ClosedXML.Excel;
using PokemonServer.DTOs;
using PokemonServer.Constants;
using PokemonServer.Exceptions;

namespace PokemonServer.Services;

public class ExcelService
{
    public byte[] GeneratePokemonExcel(List<PokemonSummaryDto> pokemons)
    {
        try
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add(AppConstants.ExcelValues.PokemonSummaryWorkSheetName);

            worksheet.Cell(1, 1).Value = AppConstants.ExcelValues.PokemonSummaryFields.Id;
            worksheet.Cell(1, 2).Value = AppConstants.ExcelValues.PokemonSummaryFields.Name;
            worksheet.Cell(1, 3).Value = AppConstants.ExcelValues.PokemonSummaryFields.Image;

            for (int i = 0; i < pokemons.Count; i++)
            {
                var pokemon = pokemons[i];
                var row = i + 2;

                worksheet.Cell(row, 1).Value = pokemon.Id;
                worksheet.Cell(row, 2).Value = pokemon.Name;
                worksheet.Cell(row, 3).Value = pokemon.Image;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        catch
        {
            throw new ExcelFileException();
        }
    }

    public byte[] GeneratePokemonExcel(PokemonDetailDto pokemon)
    {
        try
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add(AppConstants.ExcelValues.PokemonDetailWorkSheetName);

            worksheet.Cell(1, 1).Value = AppConstants.ExcelValues.PokemonDetailFields.Field;
            worksheet.Cell(1, 2).Value = AppConstants.ExcelValues.PokemonDetailFields.Value;

            worksheet.Cell(2, 1).Value = AppConstants.ExcelValues.PokemonDetailFields.Id;
            worksheet.Cell(2, 2).Value = pokemon.Id;

            worksheet.Cell(3, 1).Value = AppConstants.ExcelValues.PokemonDetailFields.Name;
            worksheet.Cell(3, 2).Value = pokemon.Name;

            worksheet.Cell(4, 1).Value = AppConstants.ExcelValues.PokemonDetailFields.Height;
            worksheet.Cell(4, 2).Value = pokemon.Height;

            worksheet.Cell(5, 1).Value = AppConstants.ExcelValues.PokemonDetailFields.Weight;
            worksheet.Cell(5, 2).Value = pokemon.Weight;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        catch
        {
            throw new ExcelFileException();
        }
    }
}