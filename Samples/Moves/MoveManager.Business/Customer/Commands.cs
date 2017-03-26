using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.Customer
{
    public class Commands
    {
        public class CustomerUpdateCommand : CommandBase<Customer, Model.Customer>
        {
            public override Guid AggregateId => this.Data.CustomerId;
        }

        public class MoveUpdateCommand : CommandBase<Customer, Model.Move>
        {
            public override Guid AggregateId => this.Data.CustomerId;
        }

        public class SurveyUpdateCommand : CommandBase<Customer, Model.Survey>
        {
            public override Guid AggregateId => this.Data.CustomerId;
        }
    }
}