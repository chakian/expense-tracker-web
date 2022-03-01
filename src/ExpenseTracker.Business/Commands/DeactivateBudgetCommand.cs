using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Commands
{
    public class DeactivateBudgetCommand : BaseCommand<DeactivateBudgetRequest, DeactivateBudgetResponse>
    {
        public DeactivateBudgetCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override DeactivateBudgetResponse HandleInternal(DeactivateBudgetRequest request, DeactivateBudgetResponse response)
        {
            // Deactivate the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            budgetBusiness.UpdateBudgetAsInactive(request);

            return response;
        }

        protected override DeactivateBudgetResponse Validate(DeactivateBudgetRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
