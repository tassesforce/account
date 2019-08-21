using System.Collections.Generic;
using compte.Accounts.List;
using compte.Models.Accounts;
using compte.Models.Accounts.Enum;

namespace compte.FIlters.Accounts
{
    public class AccountFiltersFactory<T> where T : Account
    {
        public static AccountFilters<T> BuildAccountFilters(string accountType)
        {
            AccountFilters<T> filters = null;
            AccountTypeEnum type = AccountTypeEnum.ValueOf(accountType);
            if (type != null)
            {
                if (type.Equals(AccountTypeEnum.CANDIDATE))
                {
                    filters = new CandidateAccountFilters<T>();
                } else if (type.Equals(AccountTypeEnum.AGENCY)) 
                {
                    filters = new AgencyAccountFilters<T>();
                } else if (type.Equals(AccountTypeEnum.COMPANY)) 
                {
                    filters = new CompanyAccountFilters<T>();
                }
            }
            return filters;
        }
    }
}