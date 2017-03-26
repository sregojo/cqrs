namespace MoveManager.Business.Customer
{
    public partial class Customer
    {
        public void Apply(Events.CustomerUpdatedEvent @event)
        {
            this.Model.Name = @event.Data.Name;
            this.Model.Address = @event.Data.Address;
        }

        public void Apply(Events.MoveUpdatedEvent @event)
        {
            this.Model.Moves[@event.Data.MoveId] = @event.Data;
        }

        public void Apply(Events.SurveyUpdatedEvent @event)
        {
            this.Model.Moves[@event.Data.MoveId].Survey = @event.Data;
        }
    }
}