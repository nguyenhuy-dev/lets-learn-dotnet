using DBExporter.DatabaseBuilders.SqlServer;
using DBExporter.DatabaseObjects;
using DBExporter.Options;

namespace DBExporter.DatabaseBuilders
{
    public class DatabaseBuilderFactory
    {
        public static IExportSourceBuilder? Connect(ServerTypes serverTypes, string connectionString)
        {
            return serverTypes switch
            {
                ServerTypes.SqlServer => new SqlServerDatabaseBuilder(connectionString),
                _ => null
            };
        }
    }
}
