namespace Cqrs.Helpers
{
    public class Envelope<T>
    {
        public T Data { get; set; }
    }
}