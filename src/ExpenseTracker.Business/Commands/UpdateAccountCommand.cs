using ExpenseTracker.Common.Constants;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using System.Linq;

namespace ExpenseTracker.Business.Commands
{
    public class UpdateAccountCommand : BaseCommand<UpdateAccountRequest, UpdateAccountResponse>
    {
        public UpdateAccountCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(UpdateAccountRequest request, UpdateAccountResponse response)
        {
            Account account = context.Accounts.Find(request.AccountId);

            if (account != null)
            {
                if (account.Name != request.AccountName)
                {
                    account.Name = request.AccountName;
                    AddAuditDataForUpdate(account, request.UserId);
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
        }

        protected override UpdateAccountResponse Validate(UpdateAccountRequest request)
        {
            var response = new UpdateAccountResponse();
            //TODO: Validation
            return response;
        }
    }
}
