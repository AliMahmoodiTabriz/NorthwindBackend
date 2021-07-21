using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class SuccsessDataResult<T>:DataResult<T>
    {
        public SuccsessDataResult(T data,string message,string messageId):base(data,true,message, messageId)
        {

        }
        public SuccsessDataResult(T data):base(data,true)
        {

        }
        public SuccsessDataResult(string message,string messageId) :base(default,true,message, messageId)
        {

        }
        public SuccsessDataResult():base(default,true)
        {

        }
    }
}
