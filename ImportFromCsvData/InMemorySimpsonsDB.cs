using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ImportCsvData.CsvRowEntities;
using ImportCsvData.Utils;

namespace ImportCsvData
{
    public static class InMemorySimpsonsDB
    {
        private static readonly List<SimpsonsLocationRow> _locations = new List<SimpsonsLocationRow>();
        private static readonly List<SimponsScriptLinesRow> _scriptLines = new List<SimponsScriptLinesRow>();
        private static readonly List<SimpsonsEpisodeRow> _episodes = new List<SimpsonsEpisodeRow>();
        private static readonly List<SimpsonsCharacterRow> _characters = new List<SimpsonsCharacterRow>();

        public static List<SimpsonsLocationRow> Locations => _locations;
        public static List<SimponsScriptLinesRow> ScriptLines => _scriptLines;
        public static List<SimpsonsEpisodeRow> Episodes => _episodes;
        public static List<SimpsonsCharacterRow> Characters => _characters;

        public static void LoadData()
        {
            var currentFolder = new FileInfo(typeof(Program).Assembly.Location).Directory.FullName;
            MiscUtils.ReadCsv<SimpsonsLocationRow>(currentFolder, "simpsons_locations.csv", row => _locations.Add(row));
            MiscUtils.ReadCsv<SimponsScriptLinesRow>(currentFolder, "simpsons_script_lines.csv", row => _scriptLines.Add(row));
            MiscUtils.ReadCsv<SimpsonsEpisodeRow>(currentFolder, "simpsons_episodes.csv", row => _episodes.Add(row));
            MiscUtils.ReadCsv<SimpsonsCharacterRow>(currentFolder, "simpsons_characters.csv", row => _characters.Add(row));
        }
    }
}
