using EFEntities;
using System;
using System.Linq;

namespace ExpenseTracker.Business
{
    public class TempBusiness
    {
        public void getUsers()
        {
            ExpenseTrackerContext context = new ExpenseTrackerContext();
            using (var tx = context.Database.BeginTransaction())
            {
                var users = context.Users.ToList();
                Console.WriteLine(users.ToString());
                tx.Commit();
            }
        }

        public void getCurrencies()
        {
            ExpenseTrackerContext context = new ExpenseTrackerContext();
            using (var tx = context.Database.BeginTransaction())
            {
                var currencies = context.Currencies.ToList();
                Console.WriteLine(currencies.ToString());
                tx.Commit();
            }
        }
    }
}
