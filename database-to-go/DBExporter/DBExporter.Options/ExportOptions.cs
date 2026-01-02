namespace DBExporter.Options
{
    public class ExportOptions
    {
        public ExportFormats ExportFormats { get; set; } = ExportFormats.Csv;
        public string FileName { get; set; } = "export";
        public bool ZipCompressed { get; set; } = false;
        public bool AppendExportTimeToFileName { get; set; } = false;
    }
}
