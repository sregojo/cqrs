using System;
using Cqrs.Helpers;

namespace MoveManager.Business.Customer
{
    public class Commands
    {
        public class CustomerUpdateCommand : CommandBase<CustomerAggregateRoot, Model.Customer>
        {
            public override Guid AggregateId => Data.CustomerId;
        }

        public class MoveUpdateCommand : CommandBase<CustomerAggregateRoot, Model.Move>
        {
            public override Guid AggregateId => Data.CustomerId;
        }

        public class SurveyUpdateCommand : CommandBase<CustomerAggregateRoot, Model.Survey>
        {
            public override Guid AggregateId => Data.CustomerId;
        }
    }
}