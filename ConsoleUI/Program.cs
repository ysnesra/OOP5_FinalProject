using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID : O : Open Closed Principle 
    //yeni özellik eklerken mevcut kodları değiştiremzsin prensibi

    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            //GetAllMethod(productManager);
            //GetAllByCategoryIdMethod(productManager);
            //GetByUnitPriceMethod(productManager);
            //CategoryTest();
            //GetProductDetailsMethod(productManager);
        }
        private static void GetAllMethod(ProductManager productManager)
        {
            foreach (var product in productManager.GetAll().Data)
            {
                Console.WriteLine(product.ProductName);
            }
        }
        private static void GetAllByCategoryIdMethod(ProductManager productManager)
        {
            foreach (var product in productManager.GetAllByCategoryId(2).Data)
            {
                Console.WriteLine(product.ProductName);
            }
        }
        private static void GetByUnitPriceMethod(ProductManager productManager)
        {
            foreach (var product in productManager.GetByUnitPrice(50, 100).Data)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        private static void GetProductDetailsMethod(ProductManager productManager)
        {
            var result = productManager.GetProductDetails();
            if(result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }            
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }
    }
}
