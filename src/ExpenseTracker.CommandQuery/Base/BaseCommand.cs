using ExpenseTracker.CommandQuery.Interfaces;
using ExpenseTracker.Common.Contracts;
using ExpenseTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace ExpenseTracker.CommandQuery
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
        public TResponse HandleCommand(TRequest request)
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

        /// <summary>
        /// This method is only called by other commands in this project.
        /// I am not sure if this is considered a best-practice; I'll be doing my reseach
        /// 
        /// This method doesn't do any exception handling. It trusts whichever function that is calling it, will do the handling itself.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal virtual TResponse HandleCommandInternal(TRequest request)
        {
            return HandleInternal(request);
        }

        protected abstract TResponse HandleInternal(TRequest request);
    }
}
