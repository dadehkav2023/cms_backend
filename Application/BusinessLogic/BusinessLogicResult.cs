using Application.BusinessLogic.Message;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.BusinessLogic
{
    public class BusinessLogicResult : IBusinessLogicResult
    {
        public bool Succeeded { get; }
        public IList<IPresentationMessage> Messages { get; }
        public Exception Exception { get; }
        public IList<string> ErrorFileds { get; }

        public BusinessLogicResult(bool succeeded, IEnumerable<IBusinessLogicMessage> messages = null,
            Exception exception = null, IEnumerable<string> errorFileds = null)
        {
            Succeeded = succeeded;
            Exception = exception;
            Messages = new List<IPresentationMessage>();
            if (messages == null) return;
            foreach (var message in messages)
            {
                Messages.Add(message);
            }

            ErrorFileds = new List<string>();
            if (errorFileds == null) return;
            foreach (var error in errorFileds)
            {
                ErrorFileds.Add(error);
            }
        }
    }

    public class BusinessLogicResult<TResult> : BusinessLogicResult, IBusinessLogicResult<TResult>
    {
        public TResult Result { get; }
        public int PageCount { get; }

        public BusinessLogicResult(bool succeeded, TResult result, IEnumerable<IBusinessLogicMessage> messages = null,
            Exception exception = null, int pageCount = 0, IEnumerable<string> errorFileds = null)
            : base(succeeded, messages, exception, errorFileds)
        {
            Result = result;
            PageCount = pageCount;
        }
    }
}