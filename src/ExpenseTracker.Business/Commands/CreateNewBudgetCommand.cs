using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;

namespace ExpenseTracker.Business.Commands
{
    public class CreateNewBudgetCommand : BaseCommand<CreateNewBudgetRequest, CreateNewBudgetResponse>
    {
        public CreateNewBudgetCommand(ExpenseTrackerDbContext context) : base(context)
        {
        }

        protected override void HandleInternal(CreateNewBudgetRequest request, CreateNewBudgetResponse response)
        {
            // Create the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            Budget budget = budgetBusiness.CreateNewBudget(request);

            response.CreatedBudgetId = budget.Id;
        }

        protected override CreateNewBudgetResponse Validate(CreateNewBudgetRequest request)
        {
            var response = new CreateNewBudgetResponse();
            //TODO: Validation
            return response;
        }
    }
}
