using ExpenseTracker.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

            ListModel listModel = new ListModel
            {
                TransactionList = new List<ListModel.Transaction>()
            };
            list.ForEach(l =>
            {
                listModel.TransactionList.Add(new ListModel.Transaction()
                {
                    Id = l.Id,
                    AccountName = l.AccountName,
                    CategoryName = l.CategoryName,
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
                AccountName = trx.AccountName,
                CategoryName = trx.CategoryName,
                Date = trx.Date,
                Amount = trx.Amount,
                Description = trx.Description
            };
            return View(detailModel);
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel
            {
                Date = DateTime.Now
            };

            AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
            createModel.AccountList = accountBusiness.GetAccountsOfBudget(BudgetId).Select(a => new SelectListItem(a.Name, a.Id.ToString(), false));

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            createModel.CategoryList = categoryBusiness.GetCategoriesOfBudget(BudgetId).Select(c => new SelectListItem(c.Name, c.Id.ToString(), false));

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
                TransactionBusiness.CreateNewTransaction(BudgetId, createModel.Date, createModel.AccountId, createModel.CategoryId.Value, createModel.Amount, createModel.Description, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            var trx = TransactionBusiness.GetTransactionDetails(id);

            UpdateModel updateModel = new UpdateModel()
            {
                Id = trx.Id,
                Description = trx.Description,
                AccountId = trx.AccountId,
                Amount = trx.Amount,
                BudgetId = trx.BudgetId,
                CategoryId = trx.CategoryId,
                Date = trx.Date
            };

            AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
            updateModel.AccountList = accountBusiness.GetAccountsOfBudget(BudgetId).Select(a => new SelectListItem(a.Name, a.Id.ToString(), a.Id == trx.AccountId));

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            updateModel.CategoryList = categoryBusiness.GetCategoriesOfBudget(BudgetId).Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == trx.CategoryId));

            return View(updateModel);
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateModel updateModel)
        {
            try
            {
                TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
                TransactionBusiness.UpdateTransaction(id, updateModel.Date, updateModel.AccountId, updateModel.CategoryId.Value, updateModel.Amount, updateModel.Description, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            var trx = TransactionBusiness.GetTransactionDetails(id);
            DeleteModel deleteModel = new DeleteModel()
            {
                Id = trx.Id,
                BudgetId = trx.BudgetId,
                AccountName = trx.AccountName,
                CategoryName = trx.CategoryName,
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
