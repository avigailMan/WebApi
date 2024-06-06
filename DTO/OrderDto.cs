using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDtoPost
    {
        public int Price { get; set; }

        public int Userid { get; set; }

        public virtual ICollection<OrederItem> OrederItems { get; set; } = new List<OrederItem>();

        public virtual UserDto User { get; set; } = null;
    }
}