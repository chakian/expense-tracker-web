using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class TransactionBusiness
    {
        private readonly ExpenseTrackerDbContext _context;
        private readonly AccountBusiness accountBusiness;
        public TransactionBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
            accountBusiness = new AccountBusiness(_context);
        }

        public int CreateNewTransaction(int budgetId, DateTime date, int accountId, int? targetAccountId, int categoryId, decimal amount, bool isIncome, string description, string userId)
        {
            Transaction transaction = new Transaction()
            {
                BudgetId = budgetId,
                Date = date,
                AccountId = accountId,
                CategoryId = categoryId,
                //TargetAccountId = targetAccountId,
                IsSplitTransaction = false,
                Amount = amount,
                Description = description
            };
            transaction.InsertUserId = userId;
            transaction.InsertTime = DateTime.UtcNow;
            transaction.IsActive = true;

            _context.Transactions.Add(transaction);
            accountBusiness.UpdateAccountBalanceForNewTransaction(accountId, targetAccountId, amount, isIncome, userId);

            _context.SaveChanges();

            return transaction.Id;
        }

        public List<Common.Entities.Transaction> GetTransactionsOfBudget(int budgetId)
        {
            var transactionDboList = _context.Transactions.Where(t => t.BudgetId == budgetId && t.IsActive).OrderByDescending(t => t.Date).ToList();
            List<Common.Entities.Transaction> TransactionList = new List<Common.Entities.Transaction>();

            //TODO: Get foreignKey values with the query above.
            var categoryList = _context.Categories.Where(c => c.BudgetId == budgetId).ToList();
            var accountList = _context.Accounts.Where(a => a.BudgetId == budgetId).ToList();

            transactionDboList.ForEach(tx =>
            {
                string acc = accountList.First(a => a.Id == tx.AccountId).Name;
                string cat = categoryList.First(c => c.Id == tx.CategoryId).Name;

                TransactionList.Add(new Common.Entities.Transaction()
                {
                    Id = tx.Id,
                    BudgetId = tx.BudgetId,
                    Date = tx.Date,
                    //AccountName = b.Account.Name,
                    //CategoryName = b.Category.Name,
                    AccountId = tx.AccountId,
                    AccountName = acc,

                    CategoryId = tx.CategoryId,
                    CategoryName = cat,

                    TargetAccountId = tx.TargetAccountId,
                    IsSplitTransaction = tx.IsSplitTransaction,
                    Amount = tx.Amount,
                    Description = tx.Description
                });
            });
            return TransactionList;
        }

        public List<Common.Entities.Transaction> GetTransactionsOfBudgetForPeriod(int budgetId, DateTime beginning, DateTime end)
        {
            var transactionDboList = _context.Transactions.Where(t => t.BudgetId == budgetId && t.IsActive && (t.Date >= beginning && t.Date <= end)).OrderByDescending(t => t.Date).ToList();
            List<Common.Entities.Transaction> TransactionList = new List<Common.Entities.Transaction>();

            //TODO: Get foreignKey values with the query above.
            var categoryList = _context.Categories.Where(c => c.BudgetId == budgetId).ToList();
            var accountList = _context.Accounts.Where(a => a.BudgetId == budgetId).ToList();

            transactionDboList.ForEach(tx =>
            {
                string acc = accountList.First(a => a.Id == tx.AccountId).Name;
                string cat = categoryList.First(c => c.Id == tx.CategoryId).Name;

                TransactionList.Add(new Common.Entities.Transaction()
                {
                    Id = tx.Id,
                    BudgetId = tx.BudgetId,
                    Date = tx.Date,
                    //AccountName = b.Account.Name,
                    //CategoryName = b.Category.Name,
                    AccountId = tx.AccountId,
                    AccountName = acc,

                    CategoryId = tx.CategoryId,
                    CategoryName = cat,

                    TargetAccountId = tx.TargetAccountId,
                    IsSplitTransaction = tx.IsSplitTransaction,
                    Amount = tx.Amount,
                    Description = tx.Description
                });
            });
            return TransactionList;
        }

        public Common.Entities.Transaction GetTransactionDetails(int id)
        {
            var transactionDbo = _context.Transactions.Include(t => t.Account).Include(t => t.Category).SingleOrDefault(b => b.Id == id);
            if (transactionDbo != null)
            {
                return new Common.Entities.Transaction()
                {
                    Id = transactionDbo.Id,
                    BudgetId = transactionDbo.BudgetId,
                    Date = transactionDbo.Date,
                    AccountName = transactionDbo.Account.Name,
                    CategoryName = transactionDbo.Category.Name,
                    TargetAccountId = transactionDbo.TargetAccountId,
                    IsSplitTransaction = transactionDbo.IsSplitTransaction,
                    Amount = transactionDbo.Amount,
                    Description = transactionDbo.Description
                };
            }
            else
            {
                return new Common.Entities.Transaction();
            }
        }

        public void UpdateTransaction(int transactionId, DateTime date, int accountId, int? targetAccountId, int categoryId, decimal amount, bool isIncome, string description, string userId)
        {
            Transaction transaction = _context.Transactions.Find(transactionId);

            if (transaction != null)
            {
                int oldSourceAccountId = transaction.AccountId;
                int? oldTargetAccountId = transaction.TargetAccountId;
                decimal oldTransactionAmount = transaction.Amount;
                bool oldIsIncome = transaction.IsIncome;

                transaction.Date = date;
                transaction.AccountId = accountId;
                //transaction.TargetAccountId = targetAccountId;
                transaction.CategoryId = categoryId;
                transaction.Amount = amount;
                transaction.Description = description;

                transaction.UpdateTime = DateTime.UtcNow;
                transaction.UpdateUserId = userId;

                accountBusiness.UpdateAccountBalanceForEditedTransaction(accountId, targetAccountId, amount, isIncome, oldSourceAccountId, oldTargetAccountId, oldTransactionAmount, oldIsIncome, userId);

                _context.SaveChanges();
            }
        }

        public void DeleteTransaction(int transactionId, string userId)
        {
            Transaction Transaction = _context.Transactions.Find(transactionId);

            if (Transaction != null)
            {
                accountBusiness.UpdateAccountBalanceForNewTransaction(Transaction.AccountId, Transaction.TargetAccountId, Transaction.Amount * (-1), Transaction.IsIncome, userId);

                _context.Transactions.Remove(Transaction);
                _context.SaveChanges();
            }
        }
    }
}
