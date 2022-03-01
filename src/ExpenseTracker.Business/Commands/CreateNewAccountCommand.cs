using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Commands
{
    public class CreateNewAccountCommand : BaseCommand<CreateNewAccountRequest, CreateNewAccountResponse>
    {
        public CreateNewAccountCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override CreateNewAccountResponse HandleInternal(CreateNewAccountRequest request, CreateNewAccountResponse response)
        {
            var account = new Account()
            {
                BudgetId = request.BudgetId,
                Name = request.AccountName,
                AccountType = request.AccountType,
                Balance = request.AccountBalance
            };
            AddAuditDataForCreate(account, request.UserId);

            context.Entry(account).State = EntityState.Added;
            
            context.SaveChanges();

            response.CreatedAccountId = account.Id;
            
            return response;
        }

        protected override CreateNewAccountResponse Validate(CreateNewAccountRequest request)
        {
            var response = new CreateNewAccountResponse();

            //TODO: Validation

            return response;
        }
    }
}
