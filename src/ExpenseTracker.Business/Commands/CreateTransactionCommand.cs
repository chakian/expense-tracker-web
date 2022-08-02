using ExpenseTracker.Business.Helpers;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using System;

namespace ExpenseTracker.Business.Commands
{
    public class CreateTransactionCommand : BaseCommand<CreateTransactionRequest, CreateTransactionResponse>
    {
        public CreateTransactionCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(CreateTransactionRequest request, CreateTransactionResponse response)
        {
            if (response == null) response = new CreateTransactionResponse();

            // First create a new transaction
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
            transactionBusiness.CreateTransaction(request);

            // Then update the account balance(s) accordingly
            var balanceHelper = new AccountBalanceHelper(context);
            balanceHelper.UpdateAccountBalancesForNewTransaction(request.AccountId, request.TargetAccountId, request.Amount, request.IsIncome, request.UserId);
        }

        protected override CreateTransactionResponse Validate(CreateTransactionRequest request)
        {
            var response = new CreateTransactionResponse();
            //TODO: Validation
            return response;
        }

        [Obsolete("Use MediatR or any other solution to avoid chaining comands")]
        internal void HandleCommandInternal(CreateTransactionRequest request)
        {
            HandleInternal(request, null);
        }
    }
}
