using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ImportCsvData.Utils
{
    public static class Configuration
    {
        public class SettingValues
        {
            public string[] Urls { get; set; }
            public string OpenBeerDB { get; set; }
            public string OpenBeerIngredientsDB { get; set; }
            public string SimpsonsDB { get; set; }
        }

        private static readonly Lazy<SettingValues> _instance = new Lazy<SettingValues>(InitConfiguration);
        public static SettingValues Settings => _instance.Value;

        private static SettingValues InitConfiguration()
        {
            var settings = new SettingValues();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())                   
                .AddJsonFile(Constants.ConfigurationFilename);
            builder.Build().Bind(settings);
            return settings;
        }
    }
}
