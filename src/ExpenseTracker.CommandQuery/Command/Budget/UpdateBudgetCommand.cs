using ExpenseTracker.Business;
using ExpenseTracker.CommandQuery.Base;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.CommandQuery.Command
{
    public class UpdateBudgetCommand : BaseCommand<UpdateBudgetRequest, UpdateBudgetResponse>
    {
        public UpdateBudgetCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override UpdateBudgetResponse HandleInternal(UpdateBudgetRequest request)
        {
            UpdateBudgetResponse response = new UpdateBudgetResponse();

            // Update the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            budgetBusiness.UpdateBudget(request);

            return response;
        }
    }
}
