using ExpenseTracker.Business;
using ExpenseTracker.CommandQuery.Base;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.CommandQuery.Command
{
    public class CreateNewBudgetCommand : BaseCommand<CreateNewBudgetRequest, CreateNewBudgetResponse>
    {
        public CreateNewBudgetCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override CreateNewBudgetResponse HandleInternal(CreateNewBudgetRequest request)
        {
            CreateNewBudgetResponse response = new CreateNewBudgetResponse();

            // Create the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            Budget budget = budgetBusiness.CreateNewBudget(request);

            // Assign the user to the newly created budget
            BudgetUserBusiness budgetUserBusiness = new BudgetUserBusiness(context);
            budgetUserBusiness.AddUserForBudget(budget, request.UserId);

            return response;
        }
    }
}
