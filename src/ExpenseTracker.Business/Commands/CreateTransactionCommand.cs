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

        protected override CreateTransactionResponse HandleInternal(CreateTransactionRequest request, CreateTransactionResponse response)
        {
            if (response == null) response = new CreateTransactionResponse();

            // First create a new transaction
            TransactionBusiness transactionBusiness = new TransactionBusiness(context);
            transactionBusiness.CreateTransaction(request);

            // Then update the account balance(s) accordingly
            AccountBusiness accountBusiness = new AccountBusiness(context);
            accountBusiness.UpdateAccountBalancesForNewTransaction(request.AccountId, request.TargetAccountId, request.Amount, request.IsIncome, request.UserId);

            return response;
        }

        protected override CreateTransactionResponse Validate(CreateTransactionRequest request)
        {
            throw new NotImplementedException();
        }

        [Obsolete("Use MediatR or any other solution to avoid chaining comands")]
        internal CreateTransactionResponse HandleCommandInternal(CreateTransactionRequest request)
        {
            return HandleInternal(request, null);
        }
    }
}
