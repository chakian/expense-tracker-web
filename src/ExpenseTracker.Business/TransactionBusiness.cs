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

        public int CreateNewTransaction(int budgetId, DateTime date, int accountId, int categoryId, decimal amount, string description, string userId)
        {
            Transaction transaction = new Transaction()
            {
                BudgetId = budgetId,
                Date = date,
                AccountId = accountId,
                CategoryId = categoryId,
                TargetAccountId = null,
                IsSplitTransaction = false,
                Amount = amount,
                Description = description
            };
            transaction.InsertUserId = userId;
            transaction.InsertTime = DateTime.UtcNow;
            transaction.IsActive = true;

            _context.Transactions.Add(transaction);

            _context.SaveChanges();

            return transaction.Id;
        }

        public List<Common.Entities.Transaction> GetTransactionsOfBudget(int budgetId)
        {
            var transactionDboList = _context.Transactions.Where(b => b.BudgetId == budgetId && b.IsActive).ToList();
            List<Common.Entities.Transaction> TransactionList = new List<Common.Entities.Transaction>();
            transactionDboList.ForEach(b =>
            {
                TransactionList.Add(new Common.Entities.Transaction()
                {
                    Id = b.Id,
                    BudgetId = b.BudgetId,
                    Date = b.Date,
                    AccountId = b.AccountId,
                    CategoryId = b.CategoryId,
                    TargetAccountId = b.TargetAccountId,
                    IsSplitTransaction = b.IsSplitTransaction,
                    Amount = b.Amount,
                    Description = b.Description
                });
            });
            return TransactionList;
        }

        public Common.Entities.Transaction GetTransactionDetails(int id)
        {
            var transactionDbo = _context.Transactions.SingleOrDefault(b => b.Id == id);
            if (transactionDbo != null)
            {
                return new Common.Entities.Transaction()
                {
                    Id = transactionDbo.Id,
                    BudgetId = transactionDbo.BudgetId,
                    Date = transactionDbo.Date,
                    AccountId = transactionDbo.AccountId,
                    CategoryId = transactionDbo.CategoryId,
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

        public void UpdateTransaction(int transactionId, DateTime date, int accountId, int categoryId, decimal amount, string description, string userId)
        {
            Transaction transaction = _context.Transactions.Find(transactionId);

            if (transaction != null)
            {
                transaction.Date = date;
                transaction.AccountId = accountId;
                transaction.CategoryId = categoryId;
                transaction.Amount = amount;
                transaction.Description = description;

                transaction.UpdateTime = DateTime.UtcNow;
                transaction.UpdateUserId = userId;

                _context.SaveChanges();
            }
        }

        public void DeleteTransaction(int transactionId)
        {
            Transaction Transaction = _context.Transactions.Find(transactionId);

            if (Transaction != null)
            {
                _context.Transactions.Remove(Transaction);
                _context.SaveChanges();
            }
        }
    }
}
