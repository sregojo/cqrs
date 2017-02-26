namespace SimpleCqrs.Interface
{
    public interface IApplyEvent<T>
        where T : IEvent
    {
        void Apply(T @event);
    }
}