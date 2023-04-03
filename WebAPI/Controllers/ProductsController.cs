using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]    //adresi,yolu
    [ApiController]   //Attribute'dur. ProductController classı bir ApiController'dır
    public class ProductsController : ControllerBase
    {
        //Loosely couple....gevşek bağımlılık
        //IoC Container....Inversion of Control..... değişimin kontrolü
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //Swagger: API ne zaman nereye gidiyor ile ilgili dokümantasyon sağlan üründür.Bu Apı şu durumda şu sonucu dönderir,  bu durumda bu sonucu dönderir şeklinde bilgi verir 
            var result = _productService.GetAll(); 
            if(result.Success)
            {
                return Ok(result); 
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);  //productService deki GetById
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbycategoryid")]
        public IActionResult GetAllByCategoryId(int categoryid)
        {
            var result = _productService.GetAllByCategoryId(categoryid);  
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyunitprice")]
        public IActionResult GetByUnitPrice(decimal min, decimal max)
        {
            //Postman e yazarken min max değerleri bu şekilde yazılır
            //https://localhost:44349/api/Products/getbyunitprice?min=12&max=22
            var result = _productService.GetByUnitPrice(min,max);   
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getproductdetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetProductDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
