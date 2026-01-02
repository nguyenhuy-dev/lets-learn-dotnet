using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SelectQuery { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public ServerTypes ServerTypes { get; set; } = ServerTypes.SqlServer;
    }
}
