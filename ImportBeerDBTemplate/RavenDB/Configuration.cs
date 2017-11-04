using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ImportBeerDBTemplate.RavenDB
{
    public static class Configuration
    {
        public class SettingValues
        {
            public string[] Urls { get; set; }
            public string Database { get; set; }
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
