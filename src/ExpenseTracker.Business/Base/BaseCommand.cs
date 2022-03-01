using ExpenseTracker.Interfaces.Business;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.DbModels;
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

        /// <summary>
        /// This is the main entry point for all of the commands.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TResponse Execute(TRequest request)
        {
            try
            {
                TResponse response = Validate(request);
                if (!response.HasErrors())
                {
                    response = HandleInternal(request, response);
                    context.SaveChanges();
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

        protected abstract TResponse Validate(TRequest request);
        protected abstract TResponse HandleInternal(TRequest request, TResponse response); //TODO: Make this void

        protected void AddAuditDataForCreate<T>(T entity, string userId)
            where T : BaseAuditableDbo
        {
            if(entity != null)
            {
                entity.InsertTime = DateTime.UtcNow;
                entity.InsertUserId = userId;
                entity.IsActive = true;
            }
        }

        protected void AddAuditDataForUpdate<T>(T entity, string userId)
            where T : BaseAuditableDbo
        {
            if (entity != null)
            {
                entity.UpdateTime = DateTime.UtcNow;
                entity.UpdateUserId = userId;
            }
        }
    }
}
