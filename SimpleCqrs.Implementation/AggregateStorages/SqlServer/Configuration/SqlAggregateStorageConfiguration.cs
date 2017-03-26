namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer.Configuration
{
    public class SqlAggregateStorageConfiguration : ISqlEventStorageConfiguration
    {
        public string ConnectionString =>
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        ;
    }
}