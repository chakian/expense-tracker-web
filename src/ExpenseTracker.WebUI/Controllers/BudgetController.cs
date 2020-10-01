using ExpenseTracker.WebUI.Models.Budget;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    public class BudgetController : BaseAuthenticatedController
    {
        private readonly ILogger<BudgetController> _logger;

        public BudgetController(ILogger<BudgetController> logger)
        {
            _logger = logger;
        }

        // GET: BudgetController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Budget/Index");

            ListModel listModel = new ListModel();
            listModel.BudgetList = new System.Collections.Generic.List<ListModel.Budget>();
            listModel.BudgetList.Add(new ListModel.Budget()
            {
                Id = 1,
                Name = "Test bütçe"
            });
            listModel.BudgetList.Add(new ListModel.Budget()
            {
                Id = 3,
                Name = "Test bütçe 2"
            });

            _logger.LogInformation("Finished controller action: Budget/Index");

            return View(listModel);
        }

        // GET: BudgetController/Details/5
        public ActionResult Details(int id)
        {
            DetailModel detailModel = new DetailModel()
            {
                Id = id,
                Name = "Test bütçe"
            };
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
            try
            {
                // TODO: Do the saving here
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
            //TODO: Fetch data for the given id
            UpdateModel updateModel = new UpdateModel();
            updateModel.Id = id;
            updateModel.Name = "x";
            return View(updateModel);
        }

        // POST: BudgetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateModel updateModel)
        {
            try
            {
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
            DeleteModel deleteModel = new DeleteModel();
            deleteModel.Id = id;
            deleteModel.Name = "x";
            return View(deleteModel);
        }

        // POST: BudgetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
