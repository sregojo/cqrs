namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer.Configuration
{
    public interface ISqlEventStorageConfiguration
    {
        string ConnectionString { get; }
    }
}