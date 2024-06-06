using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOrderServices
    {
        public Task<CustomHttpResponse<Order>> AddOrder(Order order);
    }
}
