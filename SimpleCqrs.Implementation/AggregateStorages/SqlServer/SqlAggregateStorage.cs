using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer.Configuration;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer
{
    public class SqlEventStorage : IEventStorage
    {
        protected readonly ISqlEventStorageConfiguration SqlEventStorageConfiguration;

        public SqlEventStorage(ISqlEventStorageConfiguration sqlEventStorageConfiguration)
        {
            this.SqlEventStorageConfiguration = sqlEventStorageConfiguration;
        }

        public T Load<T>(Guid aggregateRootId)
            where T : IEventSourced
        {
            using (var connection = new SqlConnection(this.SqlEventStorageConfiguration.ConnectionString))
            {
                connection.Open();

                if (aggregateRootId == Guid.Empty)
                    return this.NewAggregateRootInstance<T>(Guid.NewGuid());
                var aggregateRoot = this.LoadSnapshotOrNewInstance<T>(aggregateRootId, connection);

                var events = connection
                    .LoadEventsRecords(aggregateRoot.Id, aggregateRoot.Version)
                    .Deserialize();

                return (T) aggregateRoot.Apply(events);
            }
        }

        public T Store<T>(T aggregate)
            where T : IEventSourced
        {
            using (var connection = new SqlConnection(this.SqlEventStorageConfiguration.ConnectionString))
            {
                connection.Open();

                connection.StoreAggregateRootSnapShot(
                    aggregate.Id,
                    aggregate.Version,
                    JsonConvert.SerializeObject(aggregate));

                return aggregate;
            }
        }

        public IEnumerable<IPersistedEvent> Store<T>(T aggregateRoot, IEnumerable<IEvent> events)
            where T : IEventSourced
        {
            using (var connection = new SqlConnection(this.SqlEventStorageConfiguration.ConnectionString))
            {
                connection.Open();

                var version = aggregateRoot.Version;
                foreach (var @event in events)
                {
                    connection.StoreEvent(
                        aggregateRoot.Id,
                        ++version,
                        @event.GetType().AssemblyQualifiedName,
                        JsonConvert.SerializeObject(@event));

                    yield return new PersistedEvent(aggregateRoot.Id, version, @event);
                }
            }
        }

        private T LoadSnapshotOrNewInstance<T>(Guid aggregateRootId, SqlConnection connection)
        {
            T aggregateRootInstance;
            if (!connection.TryLoadAggregateSnapshot(aggregateRootId, out aggregateRootInstance))
                aggregateRootInstance = this.NewAggregateRootInstance<T>(aggregateRootId);

            return aggregateRootInstance;
        }

        private T NewAggregateRootInstance<T>(Guid aggregateRootId)
        {
            return (T) Activator.CreateInstance(typeof(T), aggregateRootId);
        }
    }
}