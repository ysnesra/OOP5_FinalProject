using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();

        IDataResult<Product> GetById(int productId);    //Tek bir ürün dönderir.Ürünün detayına bakmak istersek

        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);

        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);  //şu fiyat aralığındaki ürünleri getir

        IDataResult<List<ProductDetailDto>> GetProductDetails();

        IResult Add(Product product);
        IResult Update(Product product);

        //Transaction yönetimi yapan metot
        IResult AddTransactionalTest(Product product);
    }
}
