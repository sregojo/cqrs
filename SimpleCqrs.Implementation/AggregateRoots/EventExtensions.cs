using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateRoots
{
    public static class EventExtensions
    {
        public static bool Applies<T>(this IPersistedEvent @event, EventSourced<T> aggregateRoot)
            where T : new()
        {
            return
                aggregateRoot.Id == @event.AggregateRootId
                &&
                aggregateRoot.Version == @event.Version - 1;
        }
    }
}