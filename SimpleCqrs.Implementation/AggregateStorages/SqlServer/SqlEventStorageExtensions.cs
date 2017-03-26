using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer
{
    public static class SqlEventStorageExtensions
    {
        public static IEnumerable<EventStorageRecord> LoadEventsRecords(this SqlConnection connection,
            Guid aggregateRootId, long version)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (aggregateRootId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateRootId));
            if (version < 0) throw new ArgumentException("Value cannot be negative", nameof(connection));

            var eventsRecords = new List<EventStorageRecord>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT aggregateRootId, version, type, payload FROM EventStorage.dbo.Events WHERE aggregateRootId = @aggregateRootId AND version > @version";
                command.Parameters.AddWithValue("@aggregateRootId", aggregateRootId);
                command.Parameters.AddWithValue("@version", version);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        eventsRecords.Add(
                            new EventStorageRecord
                            {
                                AggregateRootId = new Guid(reader.GetSqlGuid(0).ToByteArray()),
                                Version = reader.GetInt32(1),
                                Type = reader.GetString(2),
                                Payload = reader.GetString(3)
                            });
                }
            }

            if (eventsRecords.Count == 0) throw new Exception($"Cannot locate events from aggregte {aggregateRootId}");

            return eventsRecords;
        }

        public static void StoreEvent(this SqlConnection connection, Guid aggregateRootId, long version, string type,
            string payload)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (aggregateRootId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateRootId));
            if (version < 0) throw new ArgumentException("Value cannot be negative", nameof(connection));
            if (string.IsNullOrEmpty(payload))
                throw new ArgumentException("Value cannot be null nor empty", nameof(payload));

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO EventStorage.dbo.Events(aggregateRootId, version, type, payload) VALUES(@aggregateRootId, @version, @type, @payload)";
                command.Parameters.AddWithValue("@aggregateRootId", aggregateRootId);
                command.Parameters.AddWithValue("@version", version);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@payload", payload);

                command.ExecuteNonQuery();
            }
        }

        public static SnapshotStorageRecord LoadAggregateSnapShotRecord(this SqlConnection connection,
            Guid aggregateRootId)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (aggregateRootId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateRootId));

            var aggregateData = new List<SnapshotStorageRecord>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT aggregateRootId, version, payload FROM EventStorage.dbo.Snapshots WHERE aggregateRootId = @aggregateRootId";
                command.Parameters.AddWithValue("@aggregateRootId", aggregateRootId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        aggregateData.Add(
                            new SnapshotStorageRecord
                            {
                                AggregateRootId = reader.GetSqlGuid(0).Value,
                                Version = reader.GetInt64(1),
                                Payload = reader.GetString(2)
                            });
                }
            }

            return aggregateData.SingleOrDefault();
        }

        public static void StoreAggregateRootSnapShot(this SqlConnection connection, Guid aggregateRootId, long version,
            string payload)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (aggregateRootId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateRootId));
            if (version < 0) throw new ArgumentException("Value cannot be negative", nameof(connection));
            if (string.IsNullOrEmpty(payload))
                throw new ArgumentException("Value cannot be null nor empty", nameof(payload));

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO EventStorage.dbo.Snapshots(aggregateRootId, version, payload) VALUES(@aggregateRootId, @version, @payload)";
                command.Parameters.AddWithValue("@aggregateRootId", aggregateRootId);
                command.Parameters.AddWithValue("@version", version);
                command.Parameters.AddWithValue("@payload", payload);

                command.ExecuteNonQuery();
            }
        }

        public static bool TryLoadAggregateSnapshot<T>(this SqlConnection connection, Guid aggregateRootId,
            out T aggregateRootInstance)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (aggregateRootId == Guid.Empty) throw new ArgumentNullException(nameof(aggregateRootId));

            var aggregateRootRecord = connection.LoadAggregateSnapShotRecord(aggregateRootId);
            if (aggregateRootRecord == null)
            {
                aggregateRootInstance = default(T);
                return false;
            }
            aggregateRootInstance = JsonConvert.DeserializeObject<T>(aggregateRootRecord.Payload);
            return true;
        }

        public static IEnumerable<IPersistedEvent> Deserialize(this IEnumerable<EventStorageRecord> eventsRecords)
        {
            var eventInstances = eventsRecords
                .Select(r => Deserialize(r.AggregateRootId, r.Version, r.Type, r.Payload))
                .OrderBy(e => e.Version);

            return eventInstances;
        }

        public static IPersistedEvent Deserialize(Guid aggregateRootId, long version, string typeName, string payload)
        {
            var eventType = Type.GetType(typeName);
            var eventInstance = (IEvent) JsonConvert.DeserializeObject(payload, eventType);
            return new PersistedEvent(aggregateRootId, version, eventInstance);
        }
    }
}