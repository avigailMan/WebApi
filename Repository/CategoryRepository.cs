using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ProductDbContext _ProductDbContext = new ProductDbContext();
        public CategoryRepository(ProductDbContext productDbContext)
        {
            this._ProductDbContext = productDbContext;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            return await _ProductDbContext.Categories.ToListAsync();
        }
}
}
