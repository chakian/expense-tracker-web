using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Budget;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetController : BaseAuthenticatedController<BudgetController>
    {
        public BudgetController(ILogger<BudgetController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options, userManager)
        {
        }

        // GET: BudgetController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Budget/Index");

            BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
            var list = budgetBusiness.GetBudgetsOfUser(UserId);

            ListModel listModel = new ListModel();
            listModel.BudgetList = new System.Collections.Generic.List<ListModel.Budget>();
            list.ForEach(l =>
            {
                listModel.BudgetList.Add(new ListModel.Budget()
                {
                    Id = l.Id,
                    Name = l.Name
                });
            });

            _logger.LogInformation("Finished controller action: Budget/Index");

            return View(listModel);
        }

        // GET: BudgetController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Started controller action: Budget/Details");

            BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
            var budget = budgetBusiness.GetBudgetDetails(id);

            DetailModel detailModel = new DetailModel()
            {
                Id = budget.Id,
                Name = budget.Name
            };

            _logger.LogInformation("Finished controller action: Budget/Details");

            return View(detailModel);
        }

        // GET: BudgetController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel();
            return View(createModel);
        }

        // POST: BudgetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModel createModel)
        {
            _logger.LogInformation("Started controller action: Budget/Create POST");

            try
            {
                // TODO: Validation
                BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
                budgetBusiness.CreateNewBudget(createModel.Name, UserId);

                _logger.LogInformation("Finished controller action: Budget/Create POST");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BudgetController/Edit/5
        public ActionResult Edit(int id)
        {
            _logger.LogInformation("Started controller action: Budget/Edit");

            BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);

            var budget = budgetBusiness.GetBudgetDetails(id);

            UpdateModel updateModel = new UpdateModel()
            {
                Id = budget.Id,
                Name = budget.Name
            };

            _logger.LogInformation("Finished controller action: Budget/Edit");

            return View(updateModel);
        }

        // POST: BudgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateModel updateModel)
        {
            _logger.LogInformation("Started controller action: Budget/Edit POST");
            try
            {
                BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
                budgetBusiness.UpdateBudget(id, updateModel.Name, UserId);

                _logger.LogInformation("Finished controller action: Budget/Edit POST");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BudgetController/Delete/5
        public ActionResult Delete(int id)
        {
            _logger.LogInformation("Started controller action: Budget/Delete");

            BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);

            var budget = budgetBusiness.GetBudgetDetails(id);

            DeleteModel deleteModel = new DeleteModel()
            {

                Id = budget.Id,
                Name = budget.Name
            };

            _logger.LogInformation("Finished controller action: Budget/Delete");

            return View(deleteModel);
        }

        // POST: BudgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _logger.LogInformation("Started controller action: Budget/Delete POST");
            try
            {
                BudgetBusiness budgetBusiness = new BudgetBusiness(_dbContextOptions);
                budgetBusiness.UpdateBudgetAsInactive(id, UserId);

                _logger.LogInformation("Finished controller action: Budget/Delete POST");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
