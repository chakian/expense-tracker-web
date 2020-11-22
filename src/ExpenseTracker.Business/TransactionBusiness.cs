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
        public TransactionBusiness(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            _context = new ExpenseTrackerDbContext(options);
        }

        public TransactionBusiness(ExpenseTrackerDbContext context)
        {
            _context = context;
        }

        public int CreateNewTransaction(Common.Entities.Transaction trx, string userId)
        {
            Transaction transaction = new Transaction()
            {
                BudgetId = trx.BudgetId,
                Date = trx.Date,
                AccountId = trx.AccountId,
                CategoryId = trx.CategoryId,
                TargetAccountId = trx.TargetAccountId,
                IsSplitTransaction = false,
                Amount = trx.Amount,
                IsIncome = trx.IsIncome,
                Description = trx.Description
            };

            transaction.InsertUserId = userId;
            transaction.InsertTime = DateTime.UtcNow;
            transaction.IsActive = true;

            _context.Transactions.Add(transaction);

            var accountBusiness = new AccountBusiness(_context);
            accountBusiness.UpdateAccountBalanceForNewTransaction(trx.AccountId, trx.TargetAccountId, trx.Amount, trx.IsIncome, userId);

            _context.SaveChanges();

            return transaction.Id;
        }

        public List<Common.Entities.Transaction> GetTransactionsOfBudgetForPeriod(int budgetId, DateTime beginning, DateTime end)
        {
            var transactionDboList = _context.Transactions.Include(t => t.Account).Include(t => t.TargetAccount).Include(t => t.Category)
                .Where(t => t.BudgetId == budgetId && t.IsActive && (t.Date >= beginning && t.Date <= end)).OrderByDescending(t => t.Date).ToList();

            List<Common.Entities.Transaction> TransactionList = new List<Common.Entities.Transaction>();

            //TODO: Get foreignKey values with the query above.
            //var categoryList = _context.Categories.Where(c => c.BudgetId == budgetId).ToList();
            //var accountList = _context.Accounts.Where(a => a.BudgetId == budgetId).ToList();

            transactionDboList.ForEach(tx =>
            {
                //string acc = accountList.First(a => a.Id == tx.AccountId).Name;
                //string cat = categoryList.First(c => c.Id == tx.CategoryId).Name;

                TransactionList.Add(new Common.Entities.Transaction()
                {
                    Id = tx.Id,
                    BudgetId = tx.BudgetId,
                    Date = tx.Date,
                    AccountId = tx.AccountId,
                    AccountName = tx.Account.Name,
                    TargetAccountId = tx.TargetAccountId,
                    TargetAccountName = tx.TargetAccount?.Name,
                    CategoryId = tx.CategoryId,
                    CategoryName = tx.Category.Name,
                    IsSplitTransaction = tx.IsSplitTransaction,
                    Amount = tx.Amount,
                    Description = tx.Description,
                    IsIncome = tx.IsIncome
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
                    AccountId = transactionDbo.AccountId,
                    AccountName = transactionDbo.Account.Name,
                    CategoryId = transactionDbo.CategoryId,
                    CategoryName = transactionDbo.Category.Name,
                    TargetAccountId = transactionDbo.TargetAccountId,
                    TargetAccountName = transactionDbo.TargetAccount?.Name,
                    IsSplitTransaction = transactionDbo.IsSplitTransaction,
                    Amount = transactionDbo.Amount,
                    IsIncome = transactionDbo.IsIncome,
                    Description = transactionDbo.Description
                };
            }
            else
            {
                return new Common.Entities.Transaction();
            }
        }

        public void UpdateTransaction(Common.Entities.Transaction tx, string userId)
        {
            Transaction transaction = _context.Transactions.Find(tx.Id);
            if (transaction != null)
            {
                int oldSourceAccountId = transaction.AccountId;
                int? oldTargetAccountId = transaction.TargetAccountId;
                decimal oldTransactionAmount = transaction.Amount;
                bool oldIsIncome = transaction.IsIncome;

                transaction.Date = tx.Date;
                transaction.AccountId = tx.AccountId;
                transaction.TargetAccountId = tx.TargetAccountId;
                transaction.CategoryId = tx.CategoryId;
                transaction.Amount = tx.Amount;
                transaction.Description = tx.Description;
                transaction.IsIncome = tx.IsIncome;

                transaction.UpdateTime = DateTime.UtcNow;
                transaction.UpdateUserId = userId;

                var accountBusiness = new AccountBusiness(_context);
                accountBusiness.UpdateAccountBalanceForEditedTransaction(tx.AccountId, tx.TargetAccountId, tx.Amount, tx.IsIncome, oldSourceAccountId, oldTargetAccountId, oldTransactionAmount, oldIsIncome, userId);

                _context.SaveChanges();
            }
        }

        public void DeleteTransaction(int transactionId, string userId)
        {
            Transaction Transaction = _context.Transactions.Find(transactionId);

            if (Transaction != null)
            {
                var accountBusiness = new AccountBusiness(_context);
                accountBusiness.UpdateAccountBalanceForNewTransaction(Transaction.AccountId, Transaction.TargetAccountId, Transaction.Amount * (-1), Transaction.IsIncome, userId);

                _context.Transactions.Remove(Transaction);
                _context.SaveChanges();
            }
        }
    }
}
