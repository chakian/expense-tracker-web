using ExpenseTracker.Business;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.CommandQuery.Command
{
    public class CreateNewAccountCommand : BaseCommand<CreateNewAccountRequest, CreateNewAccountResponse>
    {
        public CreateNewAccountCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override CreateNewAccountResponse HandleInternal(CreateNewAccountRequest request)
        {
            var response = new CreateNewAccountResponse();
            
            AccountBusiness accountBusiness = new AccountBusiness(context);
            accountBusiness.CreateNewAccount(request.BudgetId, request.AccountName, request.AccountType, request.AccountBalance, request.UserId);

            return response;
        }
    }
}
