using System.Collections.Generic;
using compte.Accounts.List;
using compte.Models.Accounts;
using Microsoft.Extensions.Configuration;

namespace compte.FIlters.Accounts
{
    public interface AccountFilters<T> where T : Account
    {
        IList<T> FilterAccounts(IEnumerable<T> comptes, ListAccountsRequest criterias, IConfiguration configuration);
    }
}