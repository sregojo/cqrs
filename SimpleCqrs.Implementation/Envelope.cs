namespace SimpleCqrs.Implementation
{
    public class Envelope<T>
    {
        public T Data { get; set; }
    }
}