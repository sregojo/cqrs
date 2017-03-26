using System;
using System.Collections.Generic;
using System.Linq;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation.AggregateRoots
{
    public abstract class EventSourced<T> : IEventSourced
        where T : new()
    {
        protected EventSourced(Guid id) : this(id, 0, new T())
        {
        }

        protected EventSourced(Guid id, long version, T model)
        {
            this.Id = id;
            this.Version = version;
            this.Model = model;
        }

        public T Model { get; protected set; }

        public Guid Id { get; protected set; }
        public long Version { get; protected set; }

        public IEventSourced Apply(IEnumerable<IPersistedEvent> events)
        {
            var applyMethod = this.GetType().GetMethod(nameof(this.ApplyEvent));
            foreach (var @event in events.OrderBy(e => e.Version))
            {
                if (!@event.Applies(this))
                    throw new Exception(
                        $"Invalid event configuration. Cannot apply event version: {@event.Version} on aggregateRoot version {this.Version}");

                applyMethod
                    .MakeGenericMethod(@event.Instance.GetType())
                    .Invoke(this, new object[] {@event.Instance});

                this.Version++;
            }

            return this;
        }

        public void ApplyEvent<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            var applier = this as IApplyEvent<TEvent>;
            if (applier == null)
                throw new InvalidOperationException(
                    $"Aggregate {this.GetType().Name} does not know how to apply event {@event.GetType().Name}");
            applier.Apply(@event);
        }
    }


    public abstract class AggregateRootBase<T> : EventSourced<T>, IAggregateRoot<T>
        where T : new()
    {
        protected AggregateRootBase(Guid aggregateRootId) : base(aggregateRootId)
        {
        }

        public IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(ICommand command)
        {
            var handler = this.GetType().GetMethod("Handle", new[] {typeof(T), command.GetType()});
            if (handler == null)
                throw new InvalidOperationException(
                    $"Aggregate {this.GetType().Name} does not know how to handle {command.GetType().Name}");

            return
                (IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>>)
                handler.Invoke(this, new object[] {this.Model, command});
        }
    }
}