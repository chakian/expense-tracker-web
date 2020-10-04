using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class AccountBusiness
    {
        private readonly ExpenseTrackerDbContext _context;
        public AccountBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
        }

        public int CreateNewAccount(int budgetId, string name, string userId)
        {
            Account account = new Account()
            {
                BudgetId = budgetId,
                Name = name
            };
            account.InsertUserId = userId;
            account.InsertTime = DateTime.UtcNow;
            account.IsActive = true;

            _context.Accounts.Add(account);

            _context.SaveChanges();

            return account.Id;
        }

        public List<Common.Entities.Account> GetAccountsOfBudget(int budgetId)
        {
            var accountDboList = _context.Accounts.Where(b => b.BudgetId == budgetId && b.IsActive).ToList();
            List<Common.Entities.Account> AccountList = new List<Common.Entities.Account>();
            accountDboList.ForEach(b =>
            {
                AccountList.Add(new Common.Entities.Account()
                {
                    Id = b.Id,
                    BudgetId = b.BudgetId,
                    Name = b.Name,
                    IsActive = b.IsActive
                });
            });
            return AccountList;
        }

        public Common.Entities.Account GetAccountDetails(int id)
        {
            var accountDbo = _context.Accounts.SingleOrDefault(b => b.Id == id);
            if (accountDbo != null)
            {
                return new Common.Entities.Account()
                {
                    Id = accountDbo.Id,
                    BudgetId = accountDbo.BudgetId,
                    Name = accountDbo.Name,
                    IsActive = accountDbo.IsActive
                };
            }
            else
            {
                return new Common.Entities.Account();
            }
        }

        public void UpdateAccount(int accountId, string name, string userId)
        {
            Account account = _context.Accounts.Find(accountId);

            if (account != null)
            {
                account.Name = name;

                account.UpdateUserId = userId;
                account.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }

        public void UpdateAccountAsInactive(int accountId, string userId)
        {
            Account Account = _context.Accounts.Find(accountId);

            if (Account != null)
            {
                Account.IsActive = false;
                Account.UpdateUserId = userId;
                Account.UpdateTime = DateTime.UtcNow;

                _context.SaveChanges();
            }
        }
    }
}
