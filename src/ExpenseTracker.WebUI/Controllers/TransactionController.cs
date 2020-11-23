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
using System.Globalization;
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
        public ActionResult Index(int month, int year, int accountId)
        {
            _logger.LogInformation("Started controller action: Transaction/Index");

            ListModel listModel = new ListModel
            {
                TransactionList = new List<ListModel.Transaction>()
            };

            if (year == 0)
            {
                year = DateTime.Now.Year;
            }
            if (month == 0)
            {
                month = DateTime.Now.Month;
            }

            listModel.StartDate = new DateTime(year, month, 1, 0, 0, 0, 0);
            listModel.EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);

            listModel.CurrentMonth = month;
            var culture = new CultureInfo("tr-TR");
            listModel.CurrentMonthName = culture.DateTimeFormat.GetMonthName(month);
            listModel.CurrentYear = year;

            TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
            //TODO: Do the account filtering in business
            var list = TransactionBusiness.GetTransactionsOfBudgetForPeriod(BudgetId, listModel.StartDate, listModel.EndDate);
            if(accountId != 0)
            {
                listModel.CurrentAccountId = accountId;
                list = list.Where(q => q.AccountId == accountId || q.TargetAccountId == accountId).ToList();
            }

            list.ForEach(l =>
            {
                listModel.TransactionList.Add(new ListModel.Transaction()
                {
                    Id = l.Id,
                    AccountName = l.AccountName,
                    TargetAccountName = l.TargetAccountName,
                    CategoryName = l.CategoryName,
                    Date = l.Date,
                    Amount = l.Amount,
                    IsIncome = l.IsIncome,
                    Description = l.Description
                });
            });

            _logger.LogInformation("Finished controller action: Transaction/Index");

            return View(listModel);
        }

        // GET: TransactionController/Create
        public ActionResult Create(int accountId)
        {
            CreateModel createModel = new CreateModel
            {
                Date = DateTime.Now
            };

            AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
            var allAccList = accountBusiness.GetAccountsOfBudget(BudgetId).ToList();

            var accList = allAccList.Select(a => new SelectListItem(a.Name, a.Id.ToString(), a.Id == accountId)).ToList();
            accList.Insert(0, new SelectListItem("Seçiniz", ""));
            createModel.AccountList = accList.AsEnumerable();

            var targetAccList = allAccList.Select(a => new SelectListItem(a.Name, a.Id.ToString(), false)).ToList();
            targetAccList.Insert(0, new SelectListItem("Seçiniz", ""));
            createModel.TargetAccountList = targetAccList.AsEnumerable();

            CategoryBusiness categoryBusiness = new CategoryBusiness(_dbContextOptions);
            var catList = categoryBusiness.GetCategoriesOfBudget(BudgetId).Select(c => new SelectListItem(c.Name, c.Id.ToString(), false)).ToList();
            catList.Insert(0, new SelectListItem("Seçiniz", ""));
            createModel.CategoryList = catList.AsEnumerable();

            createModel.ActionAccountId = accountId;

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
                if (createModel.AccountId <= 0)
                {
                    return View(createModel);
                }
                if (createModel.CategoryId.HasValue==false && createModel.IsSplitTransaction == false)
                {
                    return View(createModel);
                }

                Common.Entities.Transaction transaction = new Common.Entities.Transaction()
                {
                    BudgetId = BudgetId,
                    Date = createModel.Date,
                    AccountId = createModel.AccountId,
                    TargetAccountId = createModel.TargetAccountId,
                    CategoryId = createModel.CategoryId,
                    Amount = createModel.Amount,
                    IsIncome = createModel.IsIncome,
                    Description = createModel.Description
                };

                TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
                TransactionBusiness.CreateNewTransaction(transaction, UserId);
                return RedirectToAction(nameof(Index), new { accountId = createModel.ActionAccountId });
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
                TargetAccountId = trx.TargetAccountId,
                Amount = trx.Amount,
                IsIncome = trx.IsIncome,
                BudgetId = trx.BudgetId,
                CategoryId = trx.CategoryId,
                Date = trx.Date
            };

            AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
            var allAccList = accountBusiness.GetAccountsOfBudget(BudgetId);
            var accList = allAccList.Select(a => new SelectListItem(a.Name, a.Id.ToString(), a.Id == trx.AccountId)).ToList();
            accList.Insert(0, new SelectListItem("Seçiniz", ""));
            updateModel.AccountList = accList.AsEnumerable();

            var targetAccList = allAccList.Select(a => new SelectListItem(a.Name, a.Id.ToString(), a.Id == trx.TargetAccountId)).ToList();
            targetAccList.Insert(0, new SelectListItem("Seçiniz", ""));
            updateModel.TargetAccountList = targetAccList.AsEnumerable();

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
                //TODO: Validations
                if (updateModel.AccountId <= 0)
                {
                    return View(updateModel);
                }
                if (updateModel.CategoryId.HasValue == false && updateModel.IsSplitTransaction == false)
                {
                    return View(updateModel);
                }
                if (updateModel.AccountId == updateModel.TargetAccountId)
                {
                    return RedirectToAction("Edit", new { id = id });
                }

                Common.Entities.Transaction transaction = new Common.Entities.Transaction()
                {
                    Id = id,
                    Date = updateModel.Date,
                    AccountId = updateModel.AccountId,
                    TargetAccountId = updateModel.TargetAccountId,
                    CategoryId = updateModel.CategoryId,
                    Amount = updateModel.Amount,
                    IsIncome = updateModel.IsIncome,
                    Description = updateModel.Description
                };

                TransactionBusiness TransactionBusiness = new TransactionBusiness(_dbContextOptions);
                TransactionBusiness.UpdateTransaction(transaction, UserId);
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
                TransactionBusiness.DeleteTransaction(id, UserId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
