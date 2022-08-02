using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Common.Contracts.Query
{
    public class GetAccountsOfBudgetRequest : BaseRequest
    {
        public int BudgetId { get; set; }
    }
}
