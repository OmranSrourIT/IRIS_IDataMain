using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IRIS_API.configrationClass
{
    public class ApiResult
    {
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public Boolean IsError { get; set; }
    }
}