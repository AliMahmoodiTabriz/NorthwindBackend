using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class Result:IResult
    {
        public bool Succsess { get; }

        public string Message { get; }

        public Result(bool succsess,string message):this(succsess)
        {
            Message = message;
        }
        public Result(bool succsess)
        {
            Succsess = succsess;
        }
    }
}
