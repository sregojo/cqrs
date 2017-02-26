using System;

namespace SimpleCqrs.Implementation.AggregateStorages
{
    public class SnapshotStorageRecord
    {
        public Guid AggregateRootId { get; set; }
        public long Version { get; set; }
        public string Payload { get; set; }
    }
}