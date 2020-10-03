using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
            indexModel.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            indexModel.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            indexModel.Categories = new List<Category>();
            indexModel.Categories.Add(new Category
            {
                Name = "Main",
                BudgetedAmount = 50,
                RecordedAmount = 30,
                RemainingAmount = 20,
                SubCategories = new List<Category>()
            });
            indexModel.Categories[0].SubCategories.Add(new Category
            {
                Name = "Sub 1",
                BudgetedAmount = 35,
                RecordedAmount = 20,
                RemainingAmount = 15,
            });
            indexModel.Categories[0].SubCategories.Add(new Category
            {
                Name = "Sub 2",
                BudgetedAmount = 15,
                RecordedAmount = 10,
                RemainingAmount = 5,
            });

            _logger.LogInformation("Finished controller action: Dashboard/Index");

            return View(indexModel);
        }
    }
}
