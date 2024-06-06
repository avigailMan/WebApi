using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProduct();
        public Task<List<Product>> GetProductByDetailes(int position, int skip, string? desc, int? min, int? max, int?[] categoryId);
    }
}

