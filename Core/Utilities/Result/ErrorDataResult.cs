using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;


namespace Core.Utilities.Result
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        

        public ErrorDataResult(T data, bool success) : base(data, success)
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(string message) : base(default, false, message)
        {
        }
        public ErrorDataResult() : base(default, false)
        {

        }


    }
}
