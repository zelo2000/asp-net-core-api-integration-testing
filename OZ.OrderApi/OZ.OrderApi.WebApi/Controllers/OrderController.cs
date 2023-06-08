using Microsoft.AspNetCore.Mvc;
using OZ.OrderApi.Services.Orders;
using OZ.OrderApi.Services.Orders.Models;

namespace OZ.OrderApi.WebApi.Controllers
{
    /// <summary>
    /// Order controller
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/order")]
    [ApiVersion("1.0")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <response code="200">Returns the list of orders by user id</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> GetOrderById(Guid orderId)
        {
            var result = await _orderService.GetOrderById(orderId);
            return Ok(result);
        }

        /// <summary>
        /// Get orders by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <response code="200">Returns the order</response>
        /// <response code="404">The user is not found</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(Guid userId)
        {
            var result = await _orderService.GetOrderByUserId(userId);
            return Ok(result);
        }
    }
}