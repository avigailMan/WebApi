using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDtoPost
    {


        public string ProductName { get; set; } = null;

        public int Price { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; } = null;


    }
    public class ProductDtoGet
    {
        public int Productid { get; set; }

        public string ProductName { get; set; } = null;

        public int? Price { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

    }
    }
