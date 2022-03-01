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
