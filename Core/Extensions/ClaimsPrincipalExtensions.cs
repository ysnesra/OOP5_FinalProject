using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    //Bir kişinin Jwt den gelen claimlerini okumak için .Net deki "ClaimsPrincipal" clasını kullanırız
    //Hangi claimType veriliyorsa mesela roles'ler FindAll ile onu buluruz.
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            //? işareti null olabileceğini gösterir.Claim oluşmamış adam token istememiş olabilir
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}