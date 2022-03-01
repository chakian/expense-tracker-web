using ExpenseTracker.Business;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class BudgetBusiness : BaseBusiness
    {
        public BudgetBusiness(ExpenseTrackerDbContext context) : base(context)
        {
        }

        public Budget CreateNewBudget(CreateNewBudgetRequest request)
        {
            Budget budget = CreateNewAuditableObject<Budget>(request.UserId);
            budget.Name = request.BudgetName;

            dbContext.Budgets.Add(budget);

            return budget;
        }

        public List<Common.Entities.Budget> GetBudgetsOfUser(string userId)
        {
            var budgetUserList = dbContext.BudgetUsers.Where(bu => bu.UserId == userId && bu.IsActive).Select(bu => bu.BudgetId).ToList();
            var budgetDboList = dbContext.Budgets.Where(b => budgetUserList.Contains(b.Id) && b.IsActive).ToList();
            List<Common.Entities.Budget> budgetList = new List<Common.Entities.Budget>();
            budgetDboList.ForEach(b =>
            {
                budgetList.Add(new Common.Entities.Budget()
                {
                    Id = b.Id,
                    Name = b.Name,
                    IsActive = b.IsActive
                });
            });
            return budgetList;
        }

        public Common.Entities.Budget GetBudgetDetails(int id)
        {
            var budgetDbo = dbContext.Budgets.SingleOrDefault(b => b.Id == id);
            if (budgetDbo != null)
            {
                return new Common.Entities.Budget()
                {
                    Id = budgetDbo.Id,
                    Name = budgetDbo.Name,
                    IsActive = budgetDbo.IsActive
                };
            }
            else
            {
                return new Common.Entities.Budget();
            }
        }

        public void UpdateBudget(UpdateBudgetRequest request)
        {
            Budget budget = dbContext.Budgets.Find(request.BudgetId);

            if (budget != null)
            {
                budget.Name = request.Name;
                UpdateAuditableObject(budget, request.UserId);
            }
        }

        public void UpdateBudgetAsInactive(DeactivateBudgetRequest request)
        {
            Budget budget = dbContext.Budgets.Find(request.BudgetId);

            if (budget != null)
            {
                budget.IsActive = false;
                UpdateAuditableObject(budget, request.UserId);
            }
        }
    }
}
