using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Newtonsoft.Json;

namespace SimpleCqrs.Implementation.AggregateStorages.SqlServer
{
    public static class SqlEventStorageExtensions
    {
        public static IEnumerable<EventStorageRecord> LoadEventsRecords(this SqlConnection connection,
            Guid aggregateRootId, long version)
        {
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
                                Version         = reader.GetInt32(1),
                                Type            = reader.GetString(2),
                                Payload         = reader.GetString(3)
                            });
                }
            }

            if (eventsRecords.Count == 0) throw new Exception($"Cannot locate events from aggregte {aggregateRootId}");

            return eventsRecords;
        }

        public static void StoreEvent(this SqlConnection connection, Guid aggregateRootId, long version, string type,
            string payload)
        {
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
                                AggregateRootId = new Guid(reader.GetSqlGuid(0).ToByteArray()),
                                Version         = reader.GetInt64(1),
                                Payload         = reader.GetString(2)
                            });
                }
            }

            return aggregateData.SingleOrDefault();
        }

        public static void StoreAggregateRootSnapShot(this SqlConnection connection, Guid aggregateRootId, long version,
            string payload)
        {
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

        public static bool TryLoadAggregateSnapshot<T>(this SqlConnection connection, Guid aggregateRootId, out T aggregateRootInstance)
        {
            var aggregateRootRecord = connection.LoadAggregateSnapShotRecord(aggregateRootId);
            if (aggregateRootRecord == null)
            {
                aggregateRootInstance = default(T);
                return false;
            }
            else
            {
                aggregateRootInstance = JsonConvert.DeserializeObject<T>(aggregateRootRecord.Payload);
                return true;
            }
        }
    }
}