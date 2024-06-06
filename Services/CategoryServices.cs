using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryServices: ICategoryServices
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryServices(ICategoryRepository CategoryRepository)
        {
            this.categoryRepository = CategoryRepository;
        }


        public Task<List<Category>> GetAllCategory()
        {
            return categoryRepository.GetAllCategory();
        }
    }
}
