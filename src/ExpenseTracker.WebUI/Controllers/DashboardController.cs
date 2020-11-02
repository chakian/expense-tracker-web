using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            indexModel.StartDate = new DateTime(year, month, 1, 0, 0, 0, 0);
            indexModel.EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);

            var culture = new CultureInfo("tr-TR");
            indexModel.CurrentMonth = culture.DateTimeFormat.GetMonthName(month);
            indexModel.CurrentYear = year;

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var catList = categoryBusiness.GetCategoriesOfBudget(BudgetId);

            TransactionBusiness transactionBusiness = new TransactionBusiness(_dbContextOptions);
            var txList = transactionBusiness.GetTransactionsOfBudgetForPeriod(BudgetId, indexModel.StartDate, indexModel.EndDate);

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
