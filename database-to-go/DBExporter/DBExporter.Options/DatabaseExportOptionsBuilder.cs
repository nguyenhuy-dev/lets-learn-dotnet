using DBExporter.Helpers;

namespace DBExporter.Options
{
    /*
     * Usage: dbexport <connectionstring> <select query> [-f:<filename>] [-server:<SqlServer>] [-format:<csv|tsql>] [-compress] [-adt]
     */
    public class DatabaseExportOptionsBuilder
    {
        private readonly string[] _args;
        private readonly IEnumerable<IDatabaseExportOptionsValidator> _validators;

        public Func<DateTime> CurrentTimeFunc { get; set; } = () => DateTime.Now;
        public string FileDateTimeFormat { get; set; } = "yyyyddMM-HHmmss";

        public DatabaseExportOptionsBuilder(string[] args, IEnumerable<IDatabaseExportOptionsValidator> validators)
        {
            _args = args;
            _validators = validators ?? [];
        }

        public DatabaseExportOptions Build()
        {
            string[] strs = FilterStringAgain(_args);
            var options = Parse(strs);
            return options;
        }

        private string[] FilterStringAgain(string[] args)
        {
            return args.Select(arg => StringHelper.FilterStringAgain(arg)).ToArray();
        }

        private DatabaseExportOptions Parse(string[] args)
        {
            if (args == null || args.Length < 2)
                throw new ArgumentException("Missing required parameters.");

            var options = new DatabaseExportOptions();
            int i = 0;

            options.DatabaseOptions.ConnectionString = args[i++];
            options.DatabaseOptions.SelectQuery = args[i++];

            for (; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg.StartsWith("-f:"))
                    options.ExportOptions.FileName = arg.Substring(3);
                else if (arg.StartsWith("-server:"))
                {
                    var serverTypeName = arg.Substring(8);
                    if ("SqlServer".Equals(serverTypeName, StringComparison.OrdinalIgnoreCase))
                        options.DatabaseOptions.ServerTypes = ServerTypes.SqlServer;
                    else
                        throw new ArgumentException($"Unknown server type: {serverTypeName}");
                }
                else if (arg.StartsWith("-format:"))
                {
                    var formatName = arg.Substring(8);
                    if ("csv".Equals(formatName, StringComparison.OrdinalIgnoreCase))
                        options.ExportOptions.ExportFormats = ExportFormats.Csv;
                    else if ("tsql".Equals(formatName, StringComparison.OrdinalIgnoreCase))
                        options.ExportOptions.ExportFormats = ExportFormats.TSql;
                    else
                        throw new ArgumentException($"Unknown export format: {formatName}");
                }
                else if ("-compress".Equals(arg))
                    options.ExportOptions.ZipCompressed = true;
                else if ("-adt".Equals(arg))
                    options.ExportOptions.AppendExportTimeToFileName = true;
                else
                    throw new ArgumentException($"Unknown option: {arg}");
            }

            if (options.ExportOptions.AppendExportTimeToFileName)
                options.ExportOptions.FileName += $"-{CurrentTimeFunc().ToString(FileDateTimeFormat)}";

            if (options.ExportOptions.ExportFormats == ExportFormats.Csv)
                options.ExportOptions.FileName += ".csv";
            else if (options.ExportOptions.ExportFormats == ExportFormats.TSql)
                options.ExportOptions.FileName += ".sql";

            if (options.ExportOptions.ZipCompressed)
                options.ExportOptions.FileName += ".zip";

            return Validate(options);
        }

        private DatabaseExportOptions Validate(DatabaseExportOptions options)
        {
            foreach (var arg in _validators)
                arg.Validate(options);

            return options;
        }
    }
}
