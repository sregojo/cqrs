using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateRoots
{
    public abstract class AggregateRootBase<T> : IAggregateRoot
    {
        protected AggregateRootBase(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public T Model { get; protected set; }

        public Guid AggregateRootId { get; protected set; }
        public long Version { get; protected set; }

        public IAggregateRoot Apply(IEnumerable<IPersistedEvent> events)
        {
            var applyMethod = GetType().GetMethod(nameof(ApplyEvent));
            foreach (var @event in events.OrderBy(e => e.Version))
            {
                if (!@event.Applies(this))
                    throw new Exception(
                        $"Invalid event configuration. Cannot apply event version: {@event.Version} on aggregateRoot version {Version}");

                applyMethod
                    .MakeGenericMethod(@event.Instance.GetType())
                    .Invoke(this, new object[] {@event.Instance});

                Version++;
            }
            return this;
        }

        public void ApplyEvent<TEvent>(TEvent ev)
            where TEvent : IEvent
        {
            var applier = this as IApplyEvent<TEvent>;
            if (applier == null)
                throw new InvalidOperationException(
                    $"Aggregate {GetType().Name} does not know how to apply event {ev.GetType().Name}");
            applier.Apply(ev);
        }
    }
}