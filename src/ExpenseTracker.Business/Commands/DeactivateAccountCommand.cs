using ExpenseTracker.Business;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Commands
{
    public class DeactivateAccountCommand : BaseCommand<DeactivateAccountRequest, DeactivateAccountResponse>
    {
        public DeactivateAccountCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override DeactivateAccountResponse HandleInternal(DeactivateAccountRequest request)
        {
            var response = new DeactivateAccountResponse();

            Account account = context.Accounts.Find(request.AccountId);
            AccountBusiness accountBusiness = new AccountBusiness(context);

            if (account != null)
            {
                accountBusiness.UpdateAccountAsInactive(account, request.UserId);
            }
            else
            {
                response.AddMessage("Hesap bulunamadı.", true);
            }

            return response;
        }
    }
}
