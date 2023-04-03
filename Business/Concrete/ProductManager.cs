using Business.Abstract;
using Business.AOPDEMO;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.Concrete.DTOS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Core.Aspects.Autofac.Validation.ValidationAspect;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))]   //Add metotunu doğrula ,ProductValidator daki kurallara göre
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            //business codes
            //*1-Bir kategoride En fazla 10 ürün olabilir
            //*2-Aynı isimde ürün eklenemez
            //*3-Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez 

            //if(CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success )
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }                
            //}
            //return new ErrorResult();

            //BusinessRules   //Run ile iş kurallarının çalıştırcak

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded()); 

                        //result null gelebilir.Yani bütün kurallara uyuyordur
                        //null gelmezse hata mesajı gelmiş demektir
            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]  //IProductService deki bütün Get leri siller
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //Saat 22:00 da ürünlerin listelenmesini kapatmak istiyoruz

            if(DateTime.Now.Hour==22)   //Şuanki saat 22:00 ise Bakım zamanı
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime); 
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);          
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId== categoryId));
        }


        [CacheAspect]
        [PerformanceAspect(5)] //bu metotun çalışması 5sn'yi geçerse uyar
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));    
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        //İş kuralı parçacığı  //Bir kategoride En fazla 10 ürün olabilir
        //Categorydeki ürün sayısını doğrulama metotu  
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) 
        {
            //öncelikle parametre ile gönderilen product'ın CategoryId si aynı olanları getirip Count ile sayısısnı tutarız
            //Select count(*) from Products Where CategoryId=1 ---> arka planda bu kod çalışır.Veritabanında filtreler öyle çeker
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();  //burada SuccessResult ı boş geçeriz kulllanıcıya kuraldan geçti mesajı vermemize gerek yok
        }

        //İş kuralı parçacığı //Aynı isimde ürün eklenemez
        //Ürünismi var mı metotu
        private IResult CheckIfProductNameExists(string productName)
        {
            //Any --> var mı demek, varsa true dönderir. //bool sonuç
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();  

            if (result == true)  
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        //Categori limiti aşıldı metotu 
        //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez
        //KategoriServisini kulllanan bir ürünün olayı nasıl ele aldığı kuralı olduğu için ProductManager da yazarız
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        //Transaction yönetimi yapan metot
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {         
            //eger if sorgusunda hata alırsa önceki yaptığı ekleme işleminide geri alacak TransactionAspect ile
            Add(product);
            if (product.UnitPrice < 10)  
            {
                throw new Exception("");
            }
            Add(product);
                     
            return null;
        }
    }
}
