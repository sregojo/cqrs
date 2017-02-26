using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateRoots
{
    public static class EventExtensions
    {
        public static bool Applies<T>(this IPersistedEvent @event, AggregateRootBase<T> aggregateRoot)
        {
            return aggregateRoot.Version == @event.Version - 1;
        }
    }
}