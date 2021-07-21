using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class SuccsessResult : Result
    {
        public SuccsessResult(string message,string messageId):base(true,message, messageId)
        {

        }
        public SuccsessResult():base(true)
        {

        }
    }
}
