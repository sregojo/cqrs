using SimpleCqrs.Interface;

namespace Cqrs.Helpers
{
    public class EventBase<T> : Envelope<T>, IEvent
    {
    }
}