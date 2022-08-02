using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;

namespace ExpenseTracker.Business.Commands
{
    public class AddUserToBudgetCommand : BaseCommand<AddUserToBudgetRequest, AddUserToBudgetResponse>
    {
        public AddUserToBudgetCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(AddUserToBudgetRequest request, AddUserToBudgetResponse response)
        {
            int budgetOwnerRole = 1;
            BudgetUser budgetUser = new BudgetUser()
            {
                BudgetId = request.BudgetId,
                UserId = request.UserId,
                Role = budgetOwnerRole
            };
            AddAuditDataForCreate(budgetUser, request.UserId);

            context.Entry(budgetUser).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            context.SaveChanges();
        }

        protected override AddUserToBudgetResponse Validate(AddUserToBudgetRequest request)
        {
            var response = new AddUserToBudgetResponse();
            //TODO: Validation
            return response;
        }
    }
}
