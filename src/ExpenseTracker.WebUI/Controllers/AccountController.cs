﻿using ExpenseTracker.Business;
using ExpenseTracker.Business.Commands;
using ExpenseTracker.Business.Queries;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Common.Contracts.Query;
using ExpenseTracker.Common.Enums;
using ExpenseTracker.Common.Extensions;
using ExpenseTracker.Persistence;
using ExpenseTracker.WebUI.Models.Account;
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
    public class AccountController : BaseAuthenticatedController<AccountController>
    {
        public AccountController(ILogger<AccountController> logger, DbContextOptions<ExpenseTrackerDbContext> options, UserManager<IdentityUser> userManager)
            : base(logger, options, userManager)
        {
        }

        // GET: AccountController
        public ActionResult Index()
        {
            _logger.LogInformation("Started controller action: Account/Index");

            var request = new GetAccountsOfBudgetRequest()
            {
                BudgetId = BudgetId,
            };
            var query = new GetAccountsOfBudgetQuery(_dbContext);

            var list = query.Retrieve(request).AccountList;

            ListModel listModel = new ListModel();
            listModel.AccountList = new List<ListModel.Account>();
            list.ForEach(l =>
            {
                listModel.AccountList.Add(new ListModel.Account()
                {
                    Id = l.Id,
                    Balance = l.Balance,
                    Type = l.AccountType,
                    TypeName = AccountTypeHelper.GetAccountTypeName(l.AccountType),
                    Name = l.Name
                });
            });

            _logger.LogInformation("Finished controller action: Account/Index");

            return View(listModel);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            var request = new GetAccountDetailsRequest() { AccountId = id, UserId = UserId };
            var query = new GetAccountDetailsQuery(_dbContext);
            var acc = query.Retrieve(request).Account;

            DetailModel detailModel = new DetailModel()
            {
                Id = acc.Id,
                Name = acc.Name,
                Type = acc.AccountType,
                TypeName = AccountTypeHelper.GetAccountTypeName(acc.AccountType),
                Balance = acc.Balance
            };
            return View(detailModel);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            CreateModel createModel = new CreateModel();

            List<SelectListItem> typeList = new List<SelectListItem>();
            typeList.Add(new SelectListItem("Seçiniz", ""));
            foreach (var typeItem in Enum.GetValues(typeof(AccountType)))
            {
                AccountType type = (AccountType)typeItem;
                typeList.Add(new SelectListItem(AccountTypeHelper.GetAccountTypeName((int)type), ((int)type).ToString(), false));
            }
            createModel.TypeList = typeList.AsEnumerable();

            return View(createModel);
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateModel createModel)
        {
            try
            {
                var commandRequest = new CreateNewAccountRequest()
                {
                    BudgetId = BudgetId,
                    AccountName = createModel.Name,
                    AccountType = createModel.Type,
                    AccountBalance = createModel.Balance,
                    UserId = UserId,
                };

                var command = new CreateNewAccountCommand(_dbContext);
                int accountId = command.Execute(commandRequest).CreatedAccountId;

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
            var request = new GetAccountDetailsRequest() { AccountId = id, UserId = UserId };
            var query = new GetAccountDetailsQuery(_dbContext);
            var acc = query.Retrieve(request).Account;

            UpdateModel updateModel = new UpdateModel()
            {
                Id = acc.Id,
                Name = acc.Name,
                Balance = acc.Balance
            };
            return View(updateModel);
        }

        // POST: AccountController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, UpdateModel updateModel)
        //{
        //    try
        //    {
        //        AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
        //        accountBusiness.UpdateAccount(id, updateModel.Name, updateModel.Balance, UserId);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            var request = new GetAccountDetailsRequest() { AccountId = id, UserId = UserId };
            var query = new GetAccountDetailsQuery(_dbContext);
            var acc = query.Retrieve(request).Account;

            DeleteModel deleteModel = new DeleteModel()
            {
                Id = acc.Id,
                Name = acc.Name
                //Type = "a",
                //Balance = 502
            };
            return View(deleteModel);
        }

        // POST: AccountController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        AccountBusiness accountBusiness = new AccountBusiness(_dbContextOptions);
        //        accountBusiness.UpdateAccountAsInactive(id, UserId);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
