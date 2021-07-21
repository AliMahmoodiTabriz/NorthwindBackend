using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getlist")]
        public IActionResult GetList()
        {
            var result = _productService.GetList();

            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }

       // [Authorize(Roles = "GetById")]
        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);

            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getlistbycategoryid")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            var result = _productService.GetListByCategoryId(categoryId);

            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);

            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {            
            var result = _productService.Delete(product);
            if (!result.Succsess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
