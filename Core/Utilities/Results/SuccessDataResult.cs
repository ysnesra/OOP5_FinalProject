using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //işlem sonucunuu default olarak true vericez
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data,string message): base(data,true,message)
        {

        }
        public SuccessDataResult(T data) : base(data, true)
        {

        }
        public SuccessDataResult(string message) : base(default,true,message) //data döndermek istemeyebilir
        {

        }
        public SuccessDataResult() : base(default,true)
        {

        }
    }
}
