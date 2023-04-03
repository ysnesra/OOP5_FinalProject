using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        //Kullanıcının Claimlerini çekeceğim metot (OperationClaims-UserOperationClaims tablolarını Joinleyerek)
        List<OperationClaim> GetClaims(User user);
    }
}
