using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business.Queries
{
    public class GetAccountsOfBudgetQuery : BaseQuery<GetAccountsOfBudgetRequest, GetAccountsOfBudgetResponse>
    {
        public GetAccountsOfBudgetQuery(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(GetAccountsOfBudgetRequest request, GetAccountsOfBudgetResponse response)
        {
            var accountDboList = context.Accounts.Where(b => b.BudgetId == request.BudgetId && b.IsActive).ToList();
            List<Common.Entities.Account> accountList = new List<Common.Entities.Account>();
            accountDboList.ForEach(b =>
            {
                accountList.Add(new Common.Entities.Account()
                {
                    Id = b.Id,
                    BudgetId = b.BudgetId,
                    Name = b.Name,
                    Balance = b.Balance,
                    AccountType = b.AccountType,
                    IsActive = b.IsActive
                });
            });
            
            response.AccountList = accountList;
        }

        //TODO: Think about validation
    }
}
