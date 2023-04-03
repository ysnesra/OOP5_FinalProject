using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç

    public interface IResult
    {
        //işlemSonucu___true/false
        //işlemMesajı___kullanıcıya bilgi verecek

        bool Success { get; }  //sadece okunabilir property oluşturduk

        string Message { get; }
    }
}
