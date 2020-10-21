using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ExpenseTracker.WebUI.Controllers
{
    public class DashboardController : BaseAuthenticatedController<DashboardController>
    {
        public DashboardController(ILogger<DashboardController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options, userManager)
        {
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Started controller action: Dashboard/Index");

            IndexModel indexModel = new IndexModel();
            //indexModel.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //indexModel.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var catList = categoryBusiness.GetCategoriesOfBudget(BudgetId);

            TransactionBusiness transactionBusiness = new TransactionBusiness(_dbContextOptions);
            var txList = transactionBusiness.GetTransactionsOfBudget(BudgetId);

            indexModel.Categories = new List<Category>();
            //indexModel.Categories.Add(new Category
            //{
            //    Name = "Main",
            //    BudgetedAmount = 50,
            //    RecordedAmount = 30,
            //    RemainingAmount = 20,
            //    SubCategories = new List<Category>()
            //});
            foreach (var category in catList)
            {
                Category cat = new Category
                {
                    Name = category.Name,
                    RecordedAmount = txList.Where(t => t.CategoryId == category.Id).Sum(t => t.Amount)
                };
                indexModel.Categories.Add(cat);
            }
            
            _logger.LogInformation("Finished controller action: Dashboard/Index");

            return View(indexModel);
        }
    }
}
