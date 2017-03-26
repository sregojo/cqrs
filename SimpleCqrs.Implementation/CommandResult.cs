using System.Collections.Generic;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation
{
    public class CommandResult
    {
        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handled(IEvent @event)
        {
            return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(@event.ToEnumerable());
        }

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handled(IEnumerable<IEvent> events)
        {
            return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(events);
        }

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(ICommandError error)
        {
            return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(error.ToEnumerable());
        }

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(
            IEnumerable<ICommandError> errors)
        {
            return Either.Create<IEnumerable<ICommandError>, IEnumerable<IEvent>>(errors);
        }

        public static IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Failed(string message)
        {
            return Failed(new MessageError(message));
        }

        private class MessageError : ICommandError
        {
            public MessageError(string message)
            {
                this.Message = message;
            }

            public string Message { get; }
        }
    }
}