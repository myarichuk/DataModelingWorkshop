using System;
using System.IO;
using CsvHelper.Configuration;
using CsvReader = CsvHelper.CsvReader;

namespace ImportBeerDBTemplate.RavenUtils
{
    public static class MiscUtils
    {
        public static void ReadCsv<TRow>(
            string currentFolder,
            string filename,
            Action<TRow> rowReadCallback,
            Action<IReaderConfiguration> changeConfiguration = null)
        {
            using (var csvStream = File.OpenText(Path.Combine(currentFolder, "DataFiles", filename)))
            using (var csvReader = new CsvReader(csvStream, true))
            {
                changeConfiguration?.Invoke(csvReader.Configuration);
                if (csvReader.Read() && csvReader.ReadHeader()) //precaution
                {
                    csvReader.ValidateHeader<TRow>();
                    csvReader.Configuration.BadDataFound = null;
                    while (csvReader.Read())
                    {
                        try
                        {
                            rowReadCallback(csvReader.GetRecord<TRow>());
                        }
                        catch (Exception e)
                        {
                            /* importing for the demo,don't care about malformed rows */
                        }
                    }
                }
            }
        }
    }
}
