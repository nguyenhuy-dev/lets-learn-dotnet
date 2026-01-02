using System.Data.Common;

namespace DBExporter.DatabaseObjects
{
    public class ExportSource : IDisposable
    {
        private bool _disposedValue;
        
        public required DbConnection Connection { get; set; }
        public required DbDataReader Reader {  get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (!Reader.IsClosed)
                        Reader.Close();

                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}