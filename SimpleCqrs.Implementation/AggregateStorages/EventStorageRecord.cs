using System;

namespace SimpleCqrs.Implementation.AggregateStorages
{
    public class EventStorageRecord
    {
        public Guid AggregateRootId { get; set; }
        public long Version { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}