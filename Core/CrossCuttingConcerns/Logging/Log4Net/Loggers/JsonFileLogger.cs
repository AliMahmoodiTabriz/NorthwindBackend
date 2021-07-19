using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class JsonFileLogger: LoggerServiceBase
    {
        public JsonFileLogger():base("JsonFileLogger")
        {

        }
    }
}
