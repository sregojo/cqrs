using Cqrs.Helpers;

namespace MoveManager.Business.Customer
{
    public class Events
    {
        public class CustomerUpdatedEvent : EventBase<Model.Customer>
        {
            public CustomerUpdatedEvent(Model.Customer data)
            {
                Data = data;
            }
        }

        public class MoveUpdatedEvent : EventBase<Model.Move>
        {
            public MoveUpdatedEvent(Model.Move data)
            {
                Data = data;
            }
        }

        public class SurveyUpdatedEvent : EventBase<Model.Survey>
        {
            public SurveyUpdatedEvent(Model.Survey data)
            {
                Data = data;
            }
        }
    }
}