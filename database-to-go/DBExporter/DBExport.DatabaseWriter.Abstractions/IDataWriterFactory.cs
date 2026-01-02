namespace DBExport.DatabaseWriter.Abstractions
{
    public interface IDataWriterFactory
    {
        IDataWriter GetDataWriter();
    }
}
