using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<List<Product>> GetProductByDetailes(int position, int skip, string? desc, int? min, int? max, int?[] categoryId)
        {
            return await productRepository.GetProductByDetailes(position, skip, desc,min,max, categoryId);
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await productRepository.GetAllProduct();
        }
    }
}
