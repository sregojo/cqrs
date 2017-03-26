using SimpleCqrs.Implementation;

namespace MoveManager.Business.Customer
{
    public class Events
    {
        public class CustomerUpdatedEvent : EventBase<Model.Customer>
        {
            public CustomerUpdatedEvent(Model.Customer data)
            {
                this.Data = data;
            }
        }

        public class MoveUpdatedEvent : EventBase<Model.Move>
        {
            public MoveUpdatedEvent(Model.Move data)
            {
                this.Data = data;
            }
        }

        public class SurveyUpdatedEvent : EventBase<Model.Survey>
        {
            public SurveyUpdatedEvent(Model.Survey data)
            {
                this.Data = data;
            }
        }
    }
}