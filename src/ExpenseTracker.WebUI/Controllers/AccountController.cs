using ExpenseTracker.WebUI.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Account/Index");

            ListModel listModel = new ListModel();
            listModel.AccountList = new System.Collections.Generic.List<ListModel.Account>();
            listModel.AccountList.Add(new ListModel.Account()
            {
                Id = 1,
                Name = "Test hesap",
                Type = "a",
                Balance = 502
            });
            listModel.AccountList.Add(new ListModel.Account()
            {
                Id = 2,
                Name = "Test hesap 2",
                Type = "b",
                Balance = 11
            });

            _logger.LogInformation("Finished controller action: Account/Index");

            return View(listModel);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            DetailModel detailModel = new DetailModel()
            {
                Id = 1,
                Name = "Test hesap",
                Type = "a",
                Balance = 502
            };
            return View(detailModel);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel();
            return View(createModel);
        }

        // POST: AccountController/Create
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            //TODO: Fetch data for the given id
            UpdateModel updateModel = new UpdateModel();
            updateModel.Id = id;
            updateModel.Name = "x";
            updateModel.Type = "Y";
            updateModel.Balance = 55.98M;
            return View(updateModel);
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            DeleteModel deleteModel = new DeleteModel();
            deleteModel.Id = id;
            deleteModel.Name = "x";
            deleteModel.Type = "Y";
            deleteModel.Balance = 55.98M;
            return View(deleteModel);
        }

        // POST: AccountController/Delete/5
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
