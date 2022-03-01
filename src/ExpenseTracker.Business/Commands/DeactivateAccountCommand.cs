﻿using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;

namespace ExpenseTracker.Business.Commands
{
    public class DeactivateAccountCommand : BaseCommand<DeactivateAccountRequest, DeactivateAccountResponse>
    {
        public DeactivateAccountCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override DeactivateAccountResponse HandleInternal(DeactivateAccountRequest request, DeactivateAccountResponse response)
        {
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

        protected override DeactivateAccountResponse Validate(DeactivateAccountRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
