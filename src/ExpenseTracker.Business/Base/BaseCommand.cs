using ExpenseTracker.Interfaces.Business;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.Business
{
    public abstract class BaseCommand<TRequest, TResponse> : ICommand<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse, new()
    {
        protected readonly ExpenseTrackerDbContext context;
        public BaseCommand(ExpenseTrackerDbContext context)
        {
            this.context = context;
        }
        public BaseCommand(DbContextOptions<ExpenseTrackerDbContext> options)
        {
            context = new ExpenseTrackerDbContext(options);
        }

        /// <summary>
        /// This is the main entry point for all of the commands.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TResponse Execute(TRequest request)
        {
            try
            {
                TResponse response = HandleInternal(request);
                context.SaveChanges();
                return response;
            }
            catch (Exception exception)
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
