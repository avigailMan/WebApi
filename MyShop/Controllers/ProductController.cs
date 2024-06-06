using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {

        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<List<Product>> GetProductByDetailes([FromQuery] string? desc, [FromQuery] int? min, [FromQuery] int? max, [FromQuery] int?[] categoryId, [FromQuery] int position = 1, [FromQuery] int skip = 20)
        {

            
            var products = await productService.GetProductByDetailes(position, skip, desc, min, max, categoryId);
            return products;
          
        }
    }
}
