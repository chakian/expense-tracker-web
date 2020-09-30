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
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
            return View();
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
