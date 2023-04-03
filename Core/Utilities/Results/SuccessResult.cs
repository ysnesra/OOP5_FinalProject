using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message): base(true,message)  // base burada Result classını kastediyor
        {

        }

        //Mesaj vermek istemeyebilir.Direk true dönsün dersek;
        public SuccessResult(): base(true)   //base i tek parametre olana gider
        {

        }
    }
}
