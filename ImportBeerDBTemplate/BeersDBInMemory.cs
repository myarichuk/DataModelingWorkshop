using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using ImportBeerDBTemplate.CsvRowEntities;

namespace ImportBeerDBTemplate
{
    public static class BeersDBInMemory
    {
        private static readonly List<BeerRow> _beers = new List<BeerRow>();
        private static readonly List<BreweryRow> _breweries = new List<BreweryRow>();
        private static readonly List<BreweryGeocodeRow> _breweryGeocodes = new List<BreweryGeocodeRow>();
        private static readonly List<BeerCategoryRow> _beerCategories = new List<BeerCategoryRow>();
        private static readonly List<BeerStyleRow> _beerStyles = new List<BeerStyleRow>();

        public static IReadOnlyList<BeerRow> Beers => _beers;
        public static IReadOnlyList<BreweryRow> Breweries => _breweries;
        public static IReadOnlyList<BreweryGeocodeRow> BreweryGeocodes => _breweryGeocodes;
        public static IReadOnlyList<BeerCategoryRow> BeerCategories => _beerCategories;
        public static IReadOnlyList<BeerStyleRow> BeerStyles => _beerStyles;


        public static void LoadDataFromCsv()
        {
            var currentFolder = new FileInfo(typeof(Program).Assembly.Location).Directory.FullName;
            ReadCsv<BeerRow>(currentFolder, "beers.csv", row => _beers.Add(row), config => config.RegisterClassMap<BeerRowMap>());
            ReadCsv<BreweryRow>(currentFolder, "breweries.csv", row => _breweries.Add(row));
            ReadCsv<BreweryGeocodeRow>(currentFolder, "breweries_geocode.csv", row => _breweryGeocodes.Add(row));
            ReadCsv<BeerCategoryRow>(currentFolder, "categories.csv", row => _beerCategories.Add(row));
            ReadCsv<BeerStyleRow>(currentFolder, "styles.csv", row => _beerStyles.Add(row));
        }

        private static void ReadCsv<TRow>(
            string currentFolder,
            string filename,
            Action<TRow> rowReadCallback,
            Action<IReaderConfiguration> changeConfiguration = null)
        {
            using (var csvStream = File.OpenText(Path.Combine(currentFolder,"CsvDataFiles", filename)))
            using (var csvReader = new CsvReader(csvStream, true))
            {
                changeConfiguration?.Invoke(csvReader.Configuration);
                if (csvReader.Read() && csvReader.ReadHeader()) //precaution
                {
                    csvReader.ValidateHeader<TRow>();
                    while (csvReader.Read())
                    {
                        try
                        {
                            rowReadCallback(csvReader.GetRecord<TRow>());
                        }
                        catch (Exception)
                        {
                            /* importing for the demo,don't care about malformed rows */
                        }
                    }
                }
            }
        }
    }
}
