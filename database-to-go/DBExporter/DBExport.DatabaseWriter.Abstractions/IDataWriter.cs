using DBExporter.DatabaseObjects;

namespace DBExport.DatabaseWriter.Abstractions
{
    public interface IDataWriter
    {
        void WriteData(ExportSource database, Stream stream);
    }
}
