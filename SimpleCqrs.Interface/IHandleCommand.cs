using System.Collections.Generic;

namespace SimpleCqrs.Interface
{
    public interface IHandleCommand<TModel, ICommand>
    {
        IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(TModel model, ICommand command);
    }

    //public interface IHandleCommand<TAggregate, TModel>
    //    where TAggregate : IAggregateRoot<TModel>
    //{
    //    IEither<IEnumerable<ICommandError>, IEnumerable<IEvent>> Handle(TModel aggregate, ICommand<TAggregate> command);
    //}
}