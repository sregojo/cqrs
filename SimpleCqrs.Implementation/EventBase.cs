using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation
{
    public class EventBase<T> : Envelope<T>, IEvent
    {
    }
}