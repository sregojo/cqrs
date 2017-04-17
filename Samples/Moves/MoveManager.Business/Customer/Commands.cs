using System;
using SimpleCqrs.Implementation;

namespace MoveManager.Business.Customer
{
    public class Commands
    {
        public class CustomerUpdateCommand : CommandBase<Customer, Model.Customer>
        {
            protected override Guid DoAggregateId => this.Data.CustomerId;
        }

        public class MoveUpdateCommand : CommandBase<Customer, Model.Move>
        {
            protected override Guid DoAggregateId => this.Data.CustomerId;
        }

        public class SurveyUpdateCommand : CommandBase<Customer, Model.Survey>
        {
            protected override Guid DoAggregateId => this.Data.CustomerId;
        }
    }
}