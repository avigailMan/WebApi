using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetProductByDetailes(int position, int skip, string? desc, int? min,int? max, int?[] categoryId );
        public Task<List<Product>> GetAllProduct();

    }
}
