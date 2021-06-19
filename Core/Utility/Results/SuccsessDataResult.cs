using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class SuccsessDataResult<T>:DataResult<T>
    {
        public SuccsessDataResult(T data,string message):base(data,true,message)
        {

        }
        public SuccsessDataResult(T data):base(data,true)
        {

        }
        public SuccsessDataResult(string message):base(default,true,message)
        {

        }
        public SuccsessDataResult():base(default,true)
        {

        }
    }
}
