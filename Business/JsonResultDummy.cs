using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class JsonResultDummy
    {
        public JsonResultDummy(bool isSuccess, string message ){
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
        public bool IsSuccess { get;  }
        public string Message { get; }
    }
}
