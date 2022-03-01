﻿using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Business.Commands
{
    public class UpdateBudgetCommand : BaseCommand<UpdateBudgetRequest, UpdateBudgetResponse>
    {
        public UpdateBudgetCommand(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
        }

        protected override UpdateBudgetResponse HandleInternal(UpdateBudgetRequest request, UpdateBudgetResponse response)
        {
            // Update the budget
            BudgetBusiness budgetBusiness = new BudgetBusiness(context);
            budgetBusiness.UpdateBudget(request);

            return response;
        }

        protected override UpdateBudgetResponse Validate(UpdateBudgetRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
