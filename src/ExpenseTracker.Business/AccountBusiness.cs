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
        public AccountBusiness(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public int CreateNewAccount(int budgetId, string name, int accountType, decimal balance, string userId)
        {
            Account account = new Account()
            {
                BudgetId = budgetId,
                Name = name,
                AccountType = accountType,
                Balance = balance
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
                    Balance = b.Balance,
                    AccountType = b.AccountType,
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
                    Balance = accountDbo.Balance,
                    AccountType = accountDbo.AccountType,
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

        public void UpdateAccountBalanceForNewTransaction(int sourceAccountId, int? targetAccountId, decimal transactionAmount, bool isIncome, string userId)
        {
            Account sourceAcc, targetAcc = null;
            sourceAcc = _context.Accounts.Find(sourceAccountId);
            if (targetAccountId.HasValue)
            {
                targetAcc = _context.Accounts.Find(targetAccountId);
            }
            decimal changeAmount = transactionAmount * (isIncome ? (-1) : 1);

            sourceAcc.Balance -= changeAmount;
            sourceAcc.UpdateTime = DateTime.UtcNow;
            sourceAcc.UpdateUserId = userId;

            if (targetAcc != null)
            {
                targetAcc.Balance += changeAmount;
                targetAcc.UpdateTime = DateTime.UtcNow;
                targetAcc.UpdateUserId = userId;
            }
        }

        public void UpdateAccountBalanceForEditedTransaction(int sourceAccountId, int? targetAccountId, decimal transactionAmount, bool isIncome, int oldSourceAccountId, int? oldTargetAccountId, decimal oldTransactionAmount, bool oldIsIncome, string userId)
        {
            List<Account> accounts = new List<Account>();
            Account tempAccount = _context.Accounts.Find(sourceAccountId);
            accounts.Add(tempAccount);

            if (targetAccountId.HasValue)
            {
                tempAccount = _context.Accounts.Find(targetAccountId);
                accounts.Add(tempAccount);
            }

            if (accounts.Any(q => q.Id == oldSourceAccountId) == false)
            {
                tempAccount = _context.Accounts.Find(oldSourceAccountId);
                accounts.Add(tempAccount);
            }

            if (oldTargetAccountId.HasValue && accounts.Any(q => q.Id == oldTargetAccountId.Value) == false)
            {
                tempAccount = _context.Accounts.Find(oldTargetAccountId);
                accounts.Add(tempAccount);
            }

            Account sourceAcc = accounts.SingleOrDefault(q => q.Id == sourceAccountId),
                targetAcc = accounts.SingleOrDefault(q => q.Id == targetAccountId),
                oldSourceAcc = accounts.SingleOrDefault(q => q.Id == oldSourceAccountId),
                oldTargetAcc = accounts.SingleOrDefault(q => q.Id == oldTargetAccountId);

            decimal changeAmount = transactionAmount * (isIncome ? (-1) : 1);
            decimal oldChangeAmount = oldTransactionAmount * (oldIsIncome ? (-1) : 1);

            oldSourceAcc.Balance += oldChangeAmount;
            oldSourceAcc.UpdateTime = DateTime.UtcNow;
            oldSourceAcc.UpdateUserId = userId;
            if (oldTargetAcc != null)
            {
                oldTargetAcc.Balance -= oldChangeAmount;
                oldTargetAcc.UpdateTime = DateTime.UtcNow;
                oldTargetAcc.UpdateUserId = userId;
            }

            sourceAcc.Balance -= changeAmount;
            sourceAcc.UpdateTime = DateTime.UtcNow;
            sourceAcc.UpdateUserId = userId;
            if (targetAcc != null)
            {
                targetAcc.Balance += changeAmount;
                targetAcc.UpdateTime = DateTime.UtcNow;
                targetAcc.UpdateUserId = userId;
            }
        }
    }
}
