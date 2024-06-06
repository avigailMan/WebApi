using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using System.Reflection.Metadata.Ecma335;


namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CategoryController:ControllerBase
    {
        private readonly ICategoryServices categoryServices;
        public CategoryController(ICategoryServices CategoryServices)
        {
            this.categoryServices = CategoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            List<Category> categories = await categoryServices.GetAllCategory();
            if (categories.Count() > 0)
                return Ok(categories);
            return NotFound();
        }
    }
}
