using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message,string messageId) : base(false, message, messageId)
        {

        }
        public ErrorResult() : base(false)
        {

        }
    }
}
