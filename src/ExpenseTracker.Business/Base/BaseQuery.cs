using ExpenseTracker.Interfaces.Business;
using ExpenseTracker.Persistence;
using System;

namespace ExpenseTracker.Business
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

        public TResponse Retrieve(TRequest request)
        {
            try
            {
                TResponse response = Validate(request);
                if (!response.HasErrors())
                {
                    HandleInternal(request, response);
                }
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

        protected virtual TResponse Validate(TRequest request)
        {
            var response = new TResponse();
            return response;
        }

        protected abstract void HandleInternal(TRequest request, TResponse response);
    }
}
