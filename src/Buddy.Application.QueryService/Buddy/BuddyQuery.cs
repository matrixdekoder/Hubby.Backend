using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyQuery: IRequest<IList<BuddyReadModel>>
    {
        public BuddyQuery(Expression<Func<BuddyReadModel, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<BuddyReadModel, bool>> Predicate { get; }
    }
}
