using ExpenseTracker.CommandQuery.Interfaces;
using ExpenseTracker.Common.Contracts;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.CommandQuery.Base
{
    public abstract class BaseCommand<TRequest, TResponse> : ICommand<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse, new()
    {
        protected readonly ExpenseTrackerDbContext context;
        
        public BaseCommand(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            context = new ExpenseTrackerDbContext(options);
        }

        public TResponse HandleCommand(TRequest request)
        {
            try
            {
                TResponse response = HandleInternal((TRequest)request);
                context.SaveChanges();
                return response;
            }
            catch(Exception exception)
            {
                context.Dispose();

                TResponse response = new TResponse();
                response.WriteExceptionMessage(exception);
                return response;
            }
        }

        protected abstract TResponse HandleInternal(TRequest request);
    }
}
