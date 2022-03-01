using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Persistence;
using System.Linq;

namespace ExpenseTracker.Business.Queries
{
    public class GetAccountDetailsQuery : BaseQuery<GetAccountDetailsRequest, GetAccountDetailsResponse>
    {
        public GetAccountDetailsQuery(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(GetAccountDetailsRequest request, GetAccountDetailsResponse response)
        {
            var accountDbo = context.Accounts.SingleOrDefault(account => account.Id == request.AccountId);
            if (accountDbo != null)
            {
                var account = new Common.Entities.Account()
                {
                    Id = accountDbo.Id,
                    BudgetId = accountDbo.BudgetId,
                    Name = accountDbo.Name,
                    Balance = accountDbo.Balance,
                    AccountType = accountDbo.AccountType,
                    IsActive = accountDbo.IsActive
                };
                response.Account = account;
            }
            else
            {
                var account = new Common.Entities.Account();
                response.Account = account;
            }
        }

        //TODO: Think about validation
    }
}
