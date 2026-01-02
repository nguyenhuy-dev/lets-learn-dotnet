using CsvHelper;
using DBExport.DatabaseWriter.Abstractions;
using DBExporter.DatabaseObjects;
using System.Data.Common;
using System.Globalization;

namespace DBExporter.DatabaseWriter.Csv
{
    public class CsvDataWriter : IDataWriter
    {
        public void WriteData(ExportSource database, Stream stream)
        {
            using var writer = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            var columns = database.Reader.GetColumnSchema();
            while (database.Reader.Read())
            {
                for (int i = 0; i < columns.Count; i++)
                    csv.WriteField(database.Reader[i].ToString(), true);

                csv.NextRecord();
            }

            csv.Flush();
        }
    }
}
