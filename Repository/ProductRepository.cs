using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDbContext _ProductDbContext = new ProductDbContext();
        public ProductRepository(ProductDbContext productDbContext)
        {
            this._ProductDbContext = productDbContext;
        }
        public async Task<List<Product>> GetProductByDetailes(int position,int skip,string?desc,int? minPrice, int? maxPrice, int?[] categoryId)
        {
            var query = _ProductDbContext.Products.Where(product =>
            (desc == null ? (true) : (product.Description.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryId.Length == 0) ? (true) : (categoryId.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;
            //.Skip((position - 1) * skip)
            //.Take(skip);
            
   
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _ProductDbContext.Products.ToListAsync();
        }
    }
}
