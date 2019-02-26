using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;

namespace Buddy.Application.QueryService.Group
{
    public class GroupQuery: IRequest<IList<GroupReadModel>>
    {
        public GroupQuery(Expression<Func<GroupReadModel, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<GroupReadModel, bool>> Predicate { get; }
    }
}
