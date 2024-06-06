using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class OrderController : ControllerBase
    {
        private readonly IOrderServices orderServices;
        private readonly IMapper mapper;
        public OrderController(IOrderServices orderServices, IMapper mapper)
        {
            this.orderServices = orderServices;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] OrderDtoPost order)
        {

            var per = mapper.Map<Order>(order);
            CustomHttpResponse < Order > rdr = await orderServices.AddOrder(per);
            var orderCreateDTOs = mapper.Map<Order, OrderDtoPost>(rdr.Data);
            if (orderCreateDTOs == null)
            {
                return BadRequest();

            }
            return Ok(orderCreateDTOs);

        }
    }

        

}
