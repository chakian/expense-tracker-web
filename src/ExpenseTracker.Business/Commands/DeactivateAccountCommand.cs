using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;

namespace ExpenseTracker.Business.Commands
{
    public class DeactivateAccountCommand : BaseCommand<DeactivateAccountRequest, DeactivateAccountResponse>
    {
        public DeactivateAccountCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(DeactivateAccountRequest request, DeactivateAccountResponse response)
        {
            Account account = context.Accounts.Find(request.AccountId);
            
            if (account != null)
            {
                account.IsActive = false;
                AddAuditDataForUpdate(account, request.UserId);

                context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                response.AddMessage("Hesap bulunamadı.", true);
            }
        }

        protected override DeactivateAccountResponse Validate(DeactivateAccountRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
