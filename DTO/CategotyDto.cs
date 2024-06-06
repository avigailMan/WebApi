using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class CategotyDtoGet
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null;

        public virtual ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
    public class CategotyDtoPost
    {

        public string CategoryName { get; set; } = null;

    }
}
