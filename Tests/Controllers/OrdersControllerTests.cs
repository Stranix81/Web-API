using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Infrastructure.Interfaces;
using API.Controllers;
using Moq;
using Core.DTOs.Filters;
using Core.Models.FunctionsReturnModels;

namespace Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _controller = new OrdersController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task GetOrders_ReturnsListOfGetOrderDTO()
        {
            var orders = new List<GetOrderDTO>
            {
                new GetOrderDTO { Id = 1 },
                new GetOrderDTO { Id = 2 }
            };

            _orderServiceMock
                .Setup(s => s.GetOrdersAsync())
                .ReturnsAsync(orders);

            var actionResult = await _controller.GetOrders();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<GetOrderDTO>>(okResult.Value);
            Assert.Equal(orders.Count, response.Count);

            for (int i = 0; i < orders.Count; i++)
            {
                Assert.Equal(orders[i].Id, response[i].Id);
                Assert.Equal(orders[i].Cost, response[i].Cost);
                Assert.Equal(orders[i].Date, response[i].Date);
                Assert.Equal(orders[i].Time, response[i].Time);
                Assert.Equal(orders[i].ClientId, response[i].ClientId);
                Assert.Equal(orders[i].Status, response[i].Status);
            }
        }

        [Fact]
        public async Task GetOrders_ReturnsNotFound_WhenNoOrders()
        {
            _orderServiceMock
                .Setup(s => s.GetOrdersAsync())
                .ReturnsAsync(new List<GetOrderDTO>());

            var actionResult = await _controller.GetOrders();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No orders found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetOrder_ReturnsGetOrderDTO()
        {
            var testOrder = new GetOrderDTO { Id = 1 };

            _orderServiceMock
                .Setup(s => s.GetOrderAsync(1, false))
                .ReturnsAsync(testOrder);

            var actionResult = await _controller.GetOrder(1);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetOrder_ReturnsNotFound_WhenOrderNotExists()
        {
            _orderServiceMock
                .Setup(s => s.GetOrderAsync(999, It.IsAny<bool>()))
                .ReturnsAsync((GetOrderDTO)null!);

            var actionResult = await _controller.GetOrder(999);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Invalid ID. No order found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetOrdersFiltered_ReturnsListOfGetOrderDTO()
        {
            var testOrders = new List<GetOrderDTO> { new GetOrderDTO() };

            _orderServiceMock
                .Setup(s => s.GetOrdersFilteredAsync(It.IsAny<OrderFilterDTO>(), It.IsAny<PaginationDTO>()))
                .ReturnsAsync(testOrders);

            var actionResult = await _controller.GetOrdersFiltered(new OrderFilterDTO(), new PaginationDTO());

            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetOrdersFiltered_ReturnsNotFound_WhenNoMatches()
        {
            _orderServiceMock
                .Setup(s => s.GetOrdersFilteredAsync(It.IsAny<OrderFilterDTO>(), It.IsAny<PaginationDTO>()))
                .ReturnsAsync(new List<GetOrderDTO>());

            var actionResult = await _controller.GetOrdersFiltered(new OrderFilterDTO(), new PaginationDTO());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No orders were found for this filter", notFoundResult.Value);
        }

        [Fact]
        public async Task PostOrder_ReturnsCreatedOrder()
        {
            var testOrder = new GetOrderDTO { Id = 1 };

            _orderServiceMock
                .Setup(s => s.PostOrderAsync(It.IsAny<PostPutOrderDTO>()))
                .ReturnsAsync(testOrder);

            var actionResult = await _controller.PostOrder(new PostPutOrderDTO());

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostOrder_ReturnsBadRequest_WhenValidationError()
        {
            _orderServiceMock
                .Setup(s => s.PostOrderAsync(It.IsAny<PostPutOrderDTO>()))
                .ReturnsAsync((GetOrderDTO)null!);

            var actionResult = await _controller.PostOrder(new PostPutOrderDTO());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal("Status or client validation error", badRequestResult.Value);
        }

        [Fact]
        public async Task PutOrder_ReturnsNoContent_WhenUpdated()
        {
            _orderServiceMock
                .Setup(s => s.PutOrderAsync(1, It.IsAny<PostPutOrderDTO>()))
                .ReturnsAsync(0);

            var actionResult = await _controller.PutOrder(1, It.IsAny<PostPutOrderDTO>());

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task PutOrder_ReturnsNotFound_WhenOrderNotExists()
        {
            _orderServiceMock
                .Setup(s => s.PutOrderAsync(999, It.IsAny<PostPutOrderDTO>()))
                .ReturnsAsync(-2);

            var actionResult = await _controller.PutOrder(999, new PostPutOrderDTO());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("The order was not found", notFoundResult.Value);
        }

        [Fact]
        public async Task PutOrder_ReturnsBadRequest_WhenValidationError()
        {
            _orderServiceMock
                .Setup(s => s.PutOrderAsync(1, It.IsAny<PostPutOrderDTO>()))
                .ReturnsAsync(-1);

            var actionResult = await _controller.PutOrder(1, new PostPutOrderDTO());

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal("Status or client validation error", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNoContent_WhenDeleted()
        {
            _orderServiceMock
                .Setup(s => s.DeleteOrderAsync(1))
                .ReturnsAsync(true);

            var actionResult = await _controller.DeleteOrder(1);

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsNotFound_WhenOrderNotExists()
        {
            _orderServiceMock
                .Setup(s => s.DeleteOrderAsync(999))
                .ReturnsAsync(false);

            var actionResult = await _controller.DeleteOrder(999);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("The order was not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetCostsBdays_ReturnsListOfBdaySums()
        {
            var testBdaySums = new List<BdaySums>
            {
                new BdaySums { },
                new BdaySums { }
            };

            _orderServiceMock
                .Setup(s => s.GetCostsBdaysAsync())
                .ReturnsAsync(testBdaySums);

            var actionResult = await _controller.GetCostsBdays();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(2, ((List<BdaySums>)okResult.Value).Count);
        }

        [Fact]
        public async Task GetCostsBdays_ReturnsNotFound_WhenNoOrdersFound()
        {
            _orderServiceMock
                .Setup(s => s.GetCostsBdaysAsync())
                .ReturnsAsync(new List<BdaySums>());

            var actionResult = await _controller.GetCostsBdays();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No such orders found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAvgCostsByHour_ReturnsListOfAvgCostsByHour()
        {
            var testAvgCostsByHour = new List<AvgCostsByHour>
            {
                new AvgCostsByHour { },
                new AvgCostsByHour { }
            };

            _orderServiceMock
                .Setup(s => s.GetAvgCostsByHourAsync())
                .ReturnsAsync(testAvgCostsByHour);

            var actionResult = await _controller.GetAvgCostsByHour();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(2, ((List<AvgCostsByHour>) okResult.Value).Count);
        }

        [Fact]
        public async Task GetAvgCostsByHour_ReturnsNotFound_WhenNoListFound()
        {
            _orderServiceMock
                .Setup(s => s.GetAvgCostsByHourAsync())
                .ReturnsAsync(new List<AvgCostsByHour>());

            var actionResult = await _controller.GetAvgCostsByHour();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No such list found", notFoundResult.Value);
        }
    }
}
