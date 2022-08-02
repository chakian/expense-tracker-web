using ExpenseTracker.Interfaces.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Common.Contracts
{
    public class BaseResponse : IResponse
    {
        public List<Message> Messages { get; set; }
        public bool HasErrors()
        {
            if (Messages == null) return false;
            return Messages.Any(q => q.IsErrorMessage);
        }

        public void AddMessage(string message, bool isError, bool clearAll = false)
        {
            if (Messages == null) Messages = new List<Message>();

            if (clearAll) Messages.Clear();

            Messages.Add(new Message()
            {
                IsErrorMessage = isError,
                Text = message
            });
        }

        public void WriteExceptionMessage(Exception exception, bool clearAll = true)
        {
            AddMessage(exception.Message, true, clearAll);
        }

        public class Message
        {
            public bool IsErrorMessage { get; set; }
            public string Text { get; set; }
        }
    }
}
