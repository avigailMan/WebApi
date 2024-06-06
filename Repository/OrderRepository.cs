using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository:IOrderRepository
    {
        ProductDbContext _ProductDbContext = new ProductDbContext();
        public OrderRepository(ProductDbContext productDbContext)
        {
            this._ProductDbContext = productDbContext;
        }
        public async Task<Order> AddOrder(Order order)
        {
            await _ProductDbContext.Orders.AddAsync(order);
            await _ProductDbContext.SaveChangesAsync();
            return order;

        }
    }
}
