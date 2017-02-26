using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer
{
    public class SqlAggregateStorage : IAggregateStorage
    {
        protected readonly string ConnectionString;

        public SqlAggregateStorage(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public T Load<T>(Guid aggregateRootId)
            where T : IAggregateRoot
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                if (aggregateRootId == Guid.Empty)
                    return NewAggregateRootInstance<T>(Guid.NewGuid());

                T aggregateRootInstance = default(T);
                if (!connection.TryLoadAggregateSnapshot(aggregateRootId, out aggregateRootInstance))
                    aggregateRootInstance = NewAggregateRootInstance<T>(aggregateRootId);

                var eventsRecords = connection.LoadEventsRecords(
                    aggregateRootInstance.AggregateRootId,
                    aggregateRootInstance.Version);

                var eventInstances = eventsRecords
                    .Select(r => DeserializeEvent(r.AggregateRootId, r.Version, r.Type, r.Payload))
                    .OrderBy(e => e.Version);

                return (T) aggregateRootInstance.Apply(eventInstances);
            }
        }

        public T Store<T>(T aggregate)
            where T : IAggregateRoot
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                connection.StoreAggregateRootSnapShot(
                    aggregate.AggregateRootId,
                    aggregate.Version,
                    JsonConvert.SerializeObject(aggregate));

                return aggregate;
            }
        }

        public IEnumerable<IPersistedEvent> Store<T>(T aggregateRoot, IEnumerable<IEvent> events)
            where T : IAggregateRoot
        {
            var persistedEvents = new List<IPersistedEvent>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var version = aggregateRoot.Version;
                foreach (var @event in events)
                {
                    connection.StoreEvent(
                        aggregateRoot.AggregateRootId,
                        ++version,
                        @event.GetType().AssemblyQualifiedName,
                        JsonConvert.SerializeObject(@event));

                    persistedEvents.Add(new PersistedEvent(aggregateRoot.AggregateRootId, version, @event));
                }
            }

            return persistedEvents;
        }

        private T NewAggregateRootInstance<T>(Guid aggregateRootId)
        {
            return (T) Activator.CreateInstance(typeof(T), aggregateRootId);
        }

        private IPersistedEvent DeserializeEvent(Guid aggregateRootId, long version, string typeName, string payload)
        {
            var eventType = Type.GetType(typeName);
            var eventInstance = (IEvent) JsonConvert.DeserializeObject(payload, eventType);
            return new PersistedEvent(aggregateRootId, version, eventInstance);
        }
    }
}