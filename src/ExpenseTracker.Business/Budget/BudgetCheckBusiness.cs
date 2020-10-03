//using ExpenseTracker.Business.Base;
//using ExpenseTracker.Persistence;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;

//namespace ExpenseTracker.Business.Budget
//{
//    public class BudgetCheckBusiness : BaseBusiness
//    {
//        public BudgetCheckBusiness(DbContextOptions<ExpenseTrackerDbContext> options) : base(options) {}

//        public bool DoesUserHaveDefaultBudget(string userId)
//        {
//            var settings = context.UserSettings.FirstOrDefault(us => us.UserId.Equals(userId) && us.IsActive);
//            if(settings != null && settings.DefaultBudgetId > 0)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public bool DoesUserHaveAnyBudget(string userId)
//        {
//            ExpenseTrackerContext context = new ExpenseTrackerContext();
//            var users = context.Budgets.Any(b => b.IsActive && b.BudgetUsers.Any(bu => bu.UserId.Equals(userId)));
//            Console.WriteLine(users.ToString());
//        }
//    }
//}
