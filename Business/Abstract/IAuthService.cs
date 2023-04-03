﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //Kayıt(Üye olma) olurken Dto mdoel ve password istiyoruz
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);

        //Kullanıcı var mı
        IResult UserExists(string email);  

        //AccessToken üreten metot
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}