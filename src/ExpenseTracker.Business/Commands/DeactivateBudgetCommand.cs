using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;

namespace ExpenseTracker.Business.Commands
{
    public class DeactivateBudgetCommand : BaseCommand<DeactivateBudgetRequest, DeactivateBudgetResponse>
    {
        public DeactivateBudgetCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(DeactivateBudgetRequest request, DeactivateBudgetResponse response)
        {
            // Deactivate the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            budgetBusiness.UpdateBudgetAsInactive(request);
        }

        protected override DeactivateBudgetResponse Validate(DeactivateBudgetRequest request)
        {
            var response = new DeactivateBudgetResponse();
            //TODO: Validation
            return response;
        }
    }
}
