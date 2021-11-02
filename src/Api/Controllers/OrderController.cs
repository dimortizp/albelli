using Api.Models;
using AutoMapper;
using Core.Models;
using Core.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRequestHandler<Order,int> _createOrderHandler;
        private readonly IRequestHandler<int, Order> _getOrderHandler;
        private readonly IMapper _mapper;

        public OrderController(
            IRequestHandler<Order, int> createOrderHandler,
            IMapper mapper, 
            IRequestHandler<int, Order> getOrderHandler)
        {
            _createOrderHandler = createOrderHandler;
            _mapper = mapper;
            _getOrderHandler = getOrderHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Post([FromBody] OrderRequest order)
        {
            if(order == null)
            {
                return BadRequest();
            }

            return Ok(await _createOrderHandler.HandleAsync(_mapper.Map<Order>(order)));
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Get(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest();
            }
            var order = await _getOrderHandler.HandleAsync(orderId);
              return Ok(_mapper.Map<OrderResponse>(order));
        }
    }
}
