using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.ProductName).NotEmpty();      //ProductName'i boş olamaz [Required] zorunlu alan
            RuleFor(p=>p.ProductName).MinimumLength(2);   //ProductName'i min 2 karakter olmalıdır
            RuleFor(p=>p.UnitPrice).NotEmpty();
            RuleFor(p=>p.UnitPrice).GreaterThan(0);   //fiyat 0 dan büyük olmalı

            //içecek kategorisinin ürünlerinin fiyatı 10tl ve 10 dan az olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);

            //ürünlerin ismi a ile başlamalı gibi bir kural olursa;
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı"); //StartWithA kendi yazacağımız metot 
        }

        private bool StartWithA(string arg)  //true dönerse şartı sağlıyor demek
        {
            return arg.StartsWith("A");    //A ile balıyorsa true gelir 
        }
    }
}
