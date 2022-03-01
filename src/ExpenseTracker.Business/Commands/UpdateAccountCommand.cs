using ExpenseTracker.Common.Constants;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExpenseTracker.Business.Commands
{
    public class UpdateAccountCommand : BaseCommand<UpdateAccountRequest, UpdateAccountResponse>
    {
        public UpdateAccountCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override UpdateAccountResponse HandleInternal(UpdateAccountRequest request, UpdateAccountResponse response)
        {
            var accountBusiness = new AccountBusiness(context);
            Account account = context.Accounts.Find(request.AccountId);

            if (account != null)
            {
                if (account.Name != request.AccountName)
                {
                    accountBusiness.UpdateAccountName(account, request.AccountName, request.UserId);
                }

                if (account.Balance != request.AccountBalance)
                {
                    // Get or Create (if not exists) default category for account balance change
                    int categoryId = 0;
                    CategoryBusiness categoryBusiness = new CategoryBusiness(context);
                    var cat = categoryBusiness.GetCategoriesOfBudget(account.BudgetId).FirstOrDefault(c => c.Name == AccountConstants.DEFAULT_ACCOUNT_BALANCE_CHANGE_CATEGORY_NAME);
                    if (cat != null)
                    {
                        categoryId = cat.Id;
                    }
                    else
                    {
                        categoryId = categoryBusiness.CreateNewCategory(account.BudgetId, AccountConstants.DEFAULT_ACCOUNT_BALANCE_CHANGE_CATEGORY_NAME, null, request.UserId);
                    }

                    // Create a new transaction to fulfill balance change
                    CreateTransactionCommand createTransactionCommand = new CreateTransactionCommand(context);
                    CreateTransactionRequest createTransactionRequest = new CreateTransactionRequest()
                    {
                        BudgetId = request.BudgetId,
                        UserId = request.UserId,
                        AccountId = request.AccountId
                    };
                    createTransactionCommand.HandleCommandInternal(createTransactionRequest);
                }
            }
            else
            {
                response.Messages.Add(new Common.Contracts.BaseResponse.Message()
                {
                    IsErrorMessage = true,
                    Text = "Güncellenmeye çalışılan hesap bulunamadı."
                });
            }

            return response;
        }

        protected override UpdateAccountResponse Validate(UpdateAccountRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
