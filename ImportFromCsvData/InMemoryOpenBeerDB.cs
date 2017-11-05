using System.Collections.Generic;
using System.IO;
using ImportCsvData.CsvRowEntities;
using ImportCsvData.Utils;

namespace ImportCsvData
{
    public static class InMemoryOpenBeerDB
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


        public static void LoadData()
        {
            var currentFolder = new FileInfo(typeof(Program).Assembly.Location).Directory.FullName;
            MiscUtils.ReadCsv<BeerRow>(currentFolder, "beers.csv", row => _beers.Add(row), config => config.RegisterClassMap<BeerRowMap>());
            MiscUtils.ReadCsv<BreweryRow>(currentFolder, "breweries.csv", row => _breweries.Add(row));
            MiscUtils.ReadCsv<BreweryGeocodeRow>(currentFolder, "breweries_geocode.csv", row => _breweryGeocodes.Add(row));
            MiscUtils.ReadCsv<BeerCategoryRow>(currentFolder, "categories.csv", row => _beerCategories.Add(row));
            MiscUtils.ReadCsv<BeerStyleRow>(currentFolder, "styles.csv", row => _beerStyles.Add(row));
        }       
    }
}
