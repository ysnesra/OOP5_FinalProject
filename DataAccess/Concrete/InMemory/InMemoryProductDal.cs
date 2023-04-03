using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    { 
        List<Product> _products;    
        public InMemoryProductDal()   //constructor
        {
            //Oracle, SqlServer, Postgres, MongoDb veritabanlarından geliyormuş gibi simüle ediyoruz
            _products = new List<Product>
            {
                new Product{ ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=10},
                new Product{ ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product{ ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product{ ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product{ ProductId=5, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);    //listeye ekleriz sanki veritabanına ekliyor gibi simüle ediyoruz
        }

        public void Delete(Product product)
        {
            //1.YOL--- Foreach ile

            //Product productToDelete = null;   //Product tipinde nesne tanımlıyoruz.newleyip bellekte yer açmamıza gerek yok
            //foreach (var pro in _products)
            //{
            //    if(product.ProductId == pro.ProductId)   //listeyi tek tek dolaşarak ProductId yi kontrol ediyor
            //    {
            //        productToDelete = pro;
            //    }
            //}
            //_products.Remove(productToDelete);


            //2.YOL--- LINQ Language Integrated Query

            Product productToDelete;
            productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);

            //SingleOrDefault - listeyi tek tek dolaşıyor Şartı sağlayan TEK değeri dönderir
            //SingleOrDefault - Id ile olan aramalarda kullanılır
            //FirstOrDefault - listeyi tek tek dolaşıyor Şartı sağlayan İLK değeri dönderir
        }

        public List<Product> GetAll()      //veritabanındaki datayı Busineesa vermem lazım
        {
           return _products;    //tüm listeyi döndür
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün Id'sine sahip olan listedeki ürünü bul

            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;  //kullanıcıdan gelen ProductName'i güncelelnecek ProductName'e ata
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;  
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
            //Where içindeki şarta uyan tüm elemanları yeni bir liste haline getirip onu dönderir
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
