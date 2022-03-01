using ExpenseTracker.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Common.Contracts
{
    public class BaseResponse : IResponse
    {
        public List<Message> Messages { get; set; }
        public bool HasErrors
        {
            get
            {
                if (Messages == null) return true;
                return Messages.Any(q => q.IsErrorMessage);
            }
        }

        public void AddMessage(string message, bool isError)
        {
            if (Messages == null) Messages = new List<Message>();

            Messages.Add(new Message()
            {
                IsErrorMessage = isError,
                Text = message
            });
        }

        public void WriteExceptionMessage(Exception exception, bool clearAll = true)
        {
            if (Messages == null) Messages = new List<Message>();

            if (clearAll) Messages.Clear();

            Messages.Add(new Message()
            {
                IsErrorMessage = true,
                Text = exception.Message
            });
        }

        public class Message
        {
            public bool IsErrorMessage { get; set; }
            public string Text { get; set; }
        }
    }
}
