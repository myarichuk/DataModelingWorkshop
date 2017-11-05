namespace ImportCsvData.RavenEntities
{
    public class ScriptLine
    {
        public string Id { get; set; }
        public string Episode { get; set; }
        public int? Number { get; set; }
        public string RawText { get; set; }
        public int TimestampInMs { get; set; }
        public string SpeakingLine { get; set; }
        public string Character { get; set; }
        public string Location { get; set; }
        public string RawCharacterText { get; set; }
        public string RawLocationText { get; set; }
        public string SpokenWords { get; set; }
        public string NormalizedText { get; set; }
        public int? WordCount { get; set; }
    }
}
