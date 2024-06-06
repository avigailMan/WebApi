using Entities;
using Repository;
using System;
using Microsoft;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class OrderServices: IOrderServices
    {
        private ILogger<OrderServices> _logger;
        private readonly IOrderRepository orderRepository;
        IProductService _productService;


        public OrderServices(IProductService productService,IOrderRepository orderRepository, ILogger<OrderServices> logger)
        {
            this.orderRepository = orderRepository;
            this._logger = logger;
            this._productService = productService;
        }

        public async Task<CustomHttpResponse<Order>> AddOrder(Order order)
        {
            var products = await _productService.GetAllProduct();
            List<Product> productList = (List<Product>) products;
            var totalSum = order.OrederItems
                    .Where(oi => productList.Any(p => p.Productid == oi.Productid))
                    .Sum(oi => productList.First(p => p.Productid == oi.Productid).Price * oi.Quantity);


            if (order.Price != totalSum)
            {
                Console.WriteLine("ERR");
                _logger.LogError("נסיון פריצה!!!!!");

                return new CustomHttpResponse<Order>(null, 401); 
            }
            else
            {
                order.OrderDate = DateTime.Now;
                Order result = await orderRepository.AddOrder(order);
                return result != null ? new CustomHttpResponse<Order>(result, 200) : new CustomHttpResponse<Order>(null, 401);
            }
        }

        
    }
}
