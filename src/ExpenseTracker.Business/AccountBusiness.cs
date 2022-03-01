using ExpenseTracker.Business;
using ExpenseTracker.Common.Constants;
using ExpenseTracker.Common.Contracts.Command;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class AccountBusiness : BaseBusiness
    {
        public AccountBusiness(ExpenseTrackerDbContext context) : base(context)
        {
        }

        public AccountBusiness(DbContextOptions<ExpenseTrackerDbContext> _dbContextOptions) : base(_dbContextOptions)
        {
        }

        public int CreateNewAccount(int budgetId, string name, int accountType, decimal balance, string userId)
        {
            var account = CreateNewAuditableObject<Account>(userId);
            account.BudgetId = budgetId;
            account.Name = name;
            account.AccountType = accountType;
            account.Balance = balance;

            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();//TODO: Move to handler

            return account.Id;
        }

        public List<Common.Entities.Account> GetAccountsOfBudget(int budgetId)
        {
            var accountDboList = dbContext.Accounts.Where(b => b.BudgetId == budgetId && b.IsActive).ToList();
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
            var accountDbo = dbContext.Accounts.SingleOrDefault(b => b.Id == id);
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

        public void UpdateAccountName(Account account, string newName, string userId)
        {
            account.Name = newName;
            UpdateAuditableObject(account, userId);
        }

        public void UpdateAccountBalance(Account account, decimal balance, string userId)
        {
            TransactionBusiness transactionBusiness = new TransactionBusiness(dbContext);
            //TODO: User should be able to pick a default category for balance change transactions
            int categoryId = 0;
            CategoryBusiness categoryBusiness = new CategoryBusiness(dbContext);
            var cat = categoryBusiness.GetCategoriesOfBudget(account.BudgetId).FirstOrDefault(c => c.Name == AccountConstants.DEFAULT_ACCOUNT_BALANCE_CHANGE_CATEGORY_NAME);
            if (cat != null)
            {
                categoryId = cat.Id;
            }
            else
            {
                categoryId = categoryBusiness.CreateNewCategory(account.BudgetId, AccountConstants.DEFAULT_ACCOUNT_BALANCE_CHANGE_CATEGORY_NAME, null, userId);
            }

            decimal txAmount = account.Balance - balance;
            bool isIncome = false;
            if (txAmount < 0)
            {
                isIncome = true;
                txAmount *= -1;
            }
            var currentDay = DateTime.Now;
            var request = new CreateTransactionRequest()
            {
                AccountId = account.Id,
                Amount = txAmount,
                BudgetId = account.BudgetId,
                CategoryId = categoryId,
                Date = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, 0, 0, 0, 0),
                Description = AccountConstants.DEFAULT_ACCOUNT_BALANCE_CHANGE_DESCRIPTION,
                IsIncome = isIncome,
                UserId = userId
            };
            transactionBusiness.CreateTransaction(request);
        }

        public void UpdateAccountAsInactive(Account account, string userId)
        {
            account.IsActive = false;
            UpdateAuditableObject(account, userId);
        }

        public void UpdateAccountBalancesForNewTransaction(int sourceAccountId, int? targetAccountId, decimal transactionAmount, bool isIncome, string userId)
        {
            Account sourceAcc, targetAcc = null;
            sourceAcc = dbContext.Accounts.Find(sourceAccountId);
            if (targetAccountId.HasValue)
            {
                targetAcc = dbContext.Accounts.Find(targetAccountId);
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

        public void UpdateAccountBalancesForEditedTransaction(int sourceAccountId, int? targetAccountId, decimal transactionAmount, bool isIncome, int oldSourceAccountId, int? oldTargetAccountId, decimal oldTransactionAmount, bool oldIsIncome, string userId)
        {
            List<Account> accounts = new List<Account>();
            Account tempAccount = dbContext.Accounts.Find(sourceAccountId);
            accounts.Add(tempAccount);

            if (targetAccountId.HasValue)
            {
                tempAccount = dbContext.Accounts.Find(targetAccountId);
                accounts.Add(tempAccount);
            }

            if (accounts.Any(q => q.Id == oldSourceAccountId) == false)
            {
                tempAccount = dbContext.Accounts.Find(oldSourceAccountId);
                accounts.Add(tempAccount);
            }

            if (oldTargetAccountId.HasValue && accounts.Any(q => q.Id == oldTargetAccountId.Value) == false)
            {
                tempAccount = dbContext.Accounts.Find(oldTargetAccountId);
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
