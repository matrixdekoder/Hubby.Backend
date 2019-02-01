using System.Collections.Generic;
using Core.Domain;

namespace Account.Domain
{
    public class AccountFactory: IAggregateFactory<Account>
    {
        public Account Create(IList<IEvent> events)
        {
            var account = new Account(events);
            return account;
        }
    }
}
