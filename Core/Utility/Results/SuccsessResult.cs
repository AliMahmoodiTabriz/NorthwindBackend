using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utility.Results
{
    public class SuccsessResult : Result
    {
        public SuccsessResult(string message):base(true,message)
        {

        }
        public SuccsessResult():base(true)
        {

        }
    }
}
