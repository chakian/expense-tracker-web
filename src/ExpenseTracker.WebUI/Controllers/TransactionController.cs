using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.WebUI.Controllers
{
    public class TransactionController : BaseAuthenticatedController<TransactionController>
    {
        public TransactionController(ILogger<TransactionController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options, userManager)
        {
        }

        // GET: TransactionController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Transaction/Index");

            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            var list = TransactionBusiness.GetTransactionsOfBudget(BudgetId);

            ListModel listModel = new ListModel();
            listModel.TransactionList = new System.Collections.Generic.List<ListModel.Transaction>();
            list.ForEach(l =>
            {
                listModel.TransactionList.Add(new ListModel.Transaction()
                {
                    Id = l.Id,
                    BudgetId = l.BudgetId,
                    AccountId = l.AccountId,
                    CategoryId = l.CategoryId,
                    Date = l.Date,
                    Amount = l.Amount,
                    Description = l.Description
                });
            });

            _logger.LogInformation("Finished controller action: Transaction/Index");

            return View(listModel);
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            var trx = TransactionBusiness.GetTransactionDetails(id);
            DetailModel detailModel = new DetailModel()
            {
                Id = trx.Id,
                BudgetId = trx.BudgetId,
                AccountId = trx.AccountId,
                CategoryId = trx.CategoryId,
                Date = trx.Date,
                Amount = trx.Amount,
                Description = trx.Description
            };
            return View(detailModel);
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel();
            return View(createModel);
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModel createModel)
        {
            try
            {
                // TODO: Validations
                TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
                TransactionBusiness.CreateNewTransaction(BudgetId, createModel.Date, createModel.AccountId, createModel.AccountId, createModel.Amount, createModel.Description, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
        //    var acc = TransactionBusiness.GetTransactionDetails(id);
        //    UpdateModel updateModel = new UpdateModel()
        //    {
        //        Id = acc.Id,
        //        Name = acc.Name
        //        //Type = "a",
        //        //Balance = 502
        //    };
        //    return View(updateModel);
        //}

        // POST: TransactionController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, UpdateModel updateModel)
        //{
        //    try
        //    {
        //        TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
        //        TransactionBusiness.UpdateTransaction(id, updateModel.Name, UserId);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            var trx = TransactionBusiness.GetTransactionDetails(id);
            DeleteModel deleteModel = new DeleteModel()
            {
                Id = trx.Id,
                BudgetId = trx.BudgetId,
                AccountId = trx.AccountId,
                CategoryId = trx.CategoryId,
                Date = trx.Date,
                Amount = trx.Amount,
                Description = trx.Description
            };
            return View(deleteModel);
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
                TransactionBusiness.DeleteTransaction(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
