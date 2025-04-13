using Core.DTOs.Filters;
using Core.DTOs;
using Core.Models.FunctionsReturnModels;

namespace Infrastructure.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns><see cref="List{T}"/> list of orders/></returns>
        Task<IEnumerable<GetOrderDTO>> GetOrdersAsync();

        /// <summary>
        /// Get an order by <paramref name="id"/> (including client info depends on <paramref name="IncludeClientInfo"/>)
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <param name="IncludeClientInfo">Flag to include client info</param>
        /// <returns><see cref="GetOrderDTO"/> order, or <see langword="null"/></returns>
        Task<GetOrderDTO> GetOrderAsync(int id, bool IncludeClientInfo = false);

        /// <summary>
        /// Get orders by <paramref name="filter"/> and <paramref name="pagination"/>
        /// </summary>
        /// <param name="filter">Filter info</param>
        /// <param name="pagination">Pagination info</param>
        /// <returns><see cref="List{T}"/> list of filtered orders</returns>
        Task<IEnumerable<GetOrderDTO>> GetOrdersFilteredAsync(OrderFilterDTO filter, PaginationDTO pagination);

        /// <summary>
        /// Get the "get_total_cost_of_birthday_orders" postgres function result
        /// </summary>
        /// <returns>A <see cref="List{T}"/> list of costs</returns>
        Task<IEnumerable<BdaySums>> GetCostsBdaysAsync();

        /// <summary>
        /// Get the "get_avg_costs_by_hour" postgres function result
        /// </summary>
        /// <returns><see cref="List{T}"/> list of costs</returns>
        Task<IEnumerable<AvgCostsByHour>> GetAvgCostsByHourAsync();

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="dto">Order info</param>
        /// <returns><see cref="GetOrderDTO"/> object for success, <see langword="null"/> for validation error</returns>
        Task<GetOrderDTO> PostOrderAsync(PostPutOrderDTO dto);

        /// <summary>
        /// Edit an order by <paramref name="id"/>
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="dto">Order info</param>
        /// <returns>-1 for validation error, -2 for not found error, 0 for success</returns>
        Task<int> PutOrderAsync(int id, PostPutOrderDTO dto);

        /// <summary>
        /// Delete an order by <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see langword="true"/> for success, <see langword="false"/> for not found error</returns>
        Task<bool> DeleteOrderAsync(int id);

        bool OrderExists(int id);
    }
}
