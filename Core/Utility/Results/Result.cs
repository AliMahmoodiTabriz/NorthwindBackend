using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class Result:IResult
    {
        public bool Succsess { get; }

        public string Message { get; }
        public string MessageId { get; }
        public Result(bool succsess, string message, string messageId) : this(succsess)
        {
            Message = message;
            MessageId = messageId;
        }
        public Result(bool succsess)
        {
            Succsess = succsess;
        }
    }
}
