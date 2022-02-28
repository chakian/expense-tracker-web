using ExpenseTracker.CommandQuery.Interfaces;
using ExpenseTracker.Common.Contracts;
using ExpenseTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.CommandQuery
{
    public abstract class BaseQuery<TRequest, TResponse> : IQuery<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : IResponse, new()
    {
        protected readonly ExpenseTrackerDbContext context;
        public BaseQuery(ExpenseTrackerDbContext context)
        {
            this.context = context;
        }

        public TResponse HandleQuery(TRequest request)
        {
            try
            {
                TResponse response = HandleInternal(request);
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
