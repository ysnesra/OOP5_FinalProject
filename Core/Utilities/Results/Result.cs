using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //sadece true false döndermek isteyebilir

        public Result(bool success, string message):this(success)  //Bu constructor çalıştığında alttaki tek parametre olan constructorı da çalıştır demek
        {
            Message = message;
        }
  
        public Result(bool success)      
        {       
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }  //Message getter propertydirSadece okunabilir.Sadece Constructor içinde Set edilebilir
    }
}
