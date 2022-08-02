using System.Collections.Generic;

namespace ExpenseTracker.Common.Contracts.Query
{
    public class GetAccountsOfBudgetResponse : BaseResponse
    {
        public List<Entities.Account> AccountList { get; set; }
    }
}
