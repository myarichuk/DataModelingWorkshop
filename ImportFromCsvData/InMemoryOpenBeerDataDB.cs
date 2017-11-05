using System.Collections.Generic;
using System.IO;
using ImportCsvData.CsvRowEntities;
using ImportCsvData.Utils;

namespace ImportCsvData
{
    public static class InMemoryOpenBeerDataDB
    {
        private static readonly List<BJCPCategoryRow> _bjcp_categories = new List<BJCPCategoryRow>();
        public static IReadOnlyList<BJCPCategoryRow> BJCPCategories => _bjcp_categories;

        private static readonly List<BJCPSubCategoryRow> _bjcp_subcategories = new List<BJCPSubCategoryRow>();
        public static IReadOnlyList<BJCPSubCategoryRow> BJCPSubCategories => _bjcp_subcategories;

        private static readonly List<FermentablesRow> _fermentables = new List<FermentablesRow>();
        public static IReadOnlyList<FermentablesRow> Fermentables => _fermentables;

        private static readonly List<HopsRow> _hops = new List<HopsRow>();
        public static IReadOnlyList<HopsRow> Hops => _hops;

        private static readonly List<HopSubstituteRow> _hopSubstitutes = new List<HopSubstituteRow>();
        public static IReadOnlyList<HopSubstituteRow> HopSubstitutes => _hopSubstitutes;

        private static readonly List<SrmRow> _srm = new List<SrmRow>();
        public static IReadOnlyList<SrmRow> Srm => _srm;

        public static void LoadData()
        {
            var currentFolder = new FileInfo(typeof(Program).Assembly.Location).Directory.FullName;
            MiscUtils.ReadCsv<BJCPCategoryRow>(currentFolder, "bjcp_categories.csv", row => _bjcp_categories.Add(row));
            MiscUtils.ReadCsv<BJCPSubCategoryRow>(currentFolder, "bjcp_subcategories.csv", row => _bjcp_subcategories.Add(row));
            MiscUtils.ReadCsv<FermentablesRow>(currentFolder, "fermentables.csv", row => _fermentables.Add(row));
            MiscUtils.ReadCsv<HopsRow>(currentFolder, "hops.csv", row => _hops.Add(row));
            MiscUtils.ReadCsv<HopSubstituteRow>(currentFolder, "hop_substitutes.csv", row => _hopSubstitutes.Add(row));
            MiscUtils.ReadCsv<SrmRow>(currentFolder, "srm.csv", row => _srm.Add(row));
        }
    }
}
