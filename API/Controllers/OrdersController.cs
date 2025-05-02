using Microsoft.AspNetCore.Mvc;
using Core.Models.FunctionsReturnModels;
using Core.DTOs;
using Core.DTOs.Filters;
using Infrastructure.Interfaces;
using NSwag.Annotations;

namespace API.Controllers
{
    /// <summary>
    /// Order management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        /// <summary>
        /// Get all orders (without client info)
        /// </summary>
        /// <response code="200">All orders returned</response>>
        /// <response code="404">No orders found</response>>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDTO>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            return !orders.Any()
                ? NotFound("No orders found")
                : Ok(orders);
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get the order by its ID (with or without client info)
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="IncludeClientInfo">Flag to include client info</param>
        /// <response code="200">The order has been returned</response>>
        /// <response code="404">No such order</response>>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTO>> GetOrder(int id, [FromQuery] bool IncludeClientInfo = false)
        {
            var order = await _orderService.GetOrderAsync(id, IncludeClientInfo);

            return order == null ? NotFound("Invalid ID. No order found") : Ok(order);
        }

        /// <summary>
        /// Get orders by filter
        /// </summary>
        /// <param name="filter">Filter info</param>
        /// <param name="pagination">Pagination info</param>
        /// <response code="200">Filtered order(-s) returned</response>>
        /// <response code="404">No orders found for this filter</response>>
        /// <returns></returns>
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<GetOrderDTO>>> GetOrdersFiltered([FromQuery] OrderFilterDTO filter, [FromQuery] PaginationDTO pagination)
        {
            var orders = await _orderService.GetOrdersFilteredAsync(filter, pagination);

            return !orders.Any() 
                ? NotFound("No orders were found for this filter") 
                : Ok(orders);
        }

        // GET: api/Orders/total-cost-of-birthday-orders
        /// <summary>
        /// Get the total cost of orders with the status "Completed" made on the customer's birthday, for each customer
        /// </summary>
        /// <response code="200">A list of costs returned</response>>
        /// <response code="404">No such orders found</response>>
        /// <returns></returns>
        [HttpGet("total-cost-of-birthday-orders")]
        public async Task<ActionResult<IEnumerable<BdaySums>>> GetCostsBdays()
        {
            var costs = await _orderService.GetCostsBdaysAsync();
         
            return !costs.Any() 
                ? NotFound("No such orders found") 
                : Ok(costs);
        }

        // GET: api/Orders/average-costs-by-hour
        /// <summary>
        /// Get a list of hours from 0 to 23 with the average cost for each hour for all orders with the status "Completed" in descending order 
        /// </summary>
        /// <response code="200">A list of hours returned</response>>
        /// <response code="404">No such list found</response>>
        /// <returns></returns>
        [HttpGet("average-costs-by-hour")]
        public async Task<ActionResult<IEnumerable<AvgCostsByHour>>> GetAvgCostsByHour()
        {
            var costs = await _orderService.GetAvgCostsByHourAsync();

            return !costs.Any() 
                ? NotFound("No such list found") 
                : Ok(costs);
        }

        // POST: api/Orders
        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="dto">Order info</param>
        /// <response code="201">The order has been created</response>>
        /// <response code="400">Validation error (for the details, see the response body)</response>>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PostPutOrderDTO>> PostOrder([FromBody] PostPutOrderDTO dto)
        {
            var order = await _orderService.PostOrderAsync(dto);
         
            return order == null 
                ? BadRequest("Status or client validation error")
                : CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update the order by its ID
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="dto">Order info</param>
        /// <response code="204">The order has been updated</response>>
        /// <response code="400">Validation error (for the details, see the response body)</response>>
        /// <response code="404">The order was not found</response>>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, PostPutOrderDTO dto)
        {
            var order = await _orderService.PutOrderAsync(id, dto);

            if (order == -2)
                return NotFound("The order was not found");
            else if (order == -1)
                return BadRequest("Status or client validation error");
            else
                return NoContent();
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete the order by its ID
        /// </summary>
        /// <response code="204">The order has been deleted</response>>
        /// <response code="404">The order was not found</response>>
        /// <param name="id">Order ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, typeof(void), Description = "Removed successfully")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.DeleteOrderAsync(id);

            return order == false 
                ? NotFound("The order was not found") 
                : NoContent();
        }
    }
}
