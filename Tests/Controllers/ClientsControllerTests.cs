using Microsoft.AspNetCore.Mvc;
using Core.DTOs;
using Infrastructure.Interfaces;
using API.Controllers;
using Moq;
using Core.DTOs.Filters;

namespace Tests.Controllers
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _clientServiceMock = new Mock<IClientService>();
            _controller = new ClientsController(_clientServiceMock.Object);
        }

        [Fact]
        public async Task GetClients_ReturnsListOfGetClientDTO()
        {
            var clients = new List<GetClientDTO>
            {
                new GetClientDTO { id = 1 },
                new GetClientDTO { id = 2 }
            };

            _clientServiceMock
                .Setup(s => s.GetClientsAsync())
                .ReturnsAsync(clients);

            var actionResult = await _controller.GetClients();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<GetClientDTO>>(okResult.Value);
            Assert.Equal(clients.Count, response.Count);

            for(int i = 0; i < clients.Count; i++)
            {
                Assert.Equal(clients[i].id, response[i].id);
                Assert.Equal(clients[i].name, response[i].name);
                Assert.Equal(clients[i].lastname, response[i].lastname);
                Assert.Equal(clients[i].birth_date, response[i].birth_date);
            }
        }

        [Fact]
        public async Task GetClients_ReturnsNotFound_WhenNoClients()
        {
            _clientServiceMock
                .Setup(s => s.GetClientsAsync())
                .ReturnsAsync(new List<GetClientDTO>());

            var actionResult = await _controller.GetClients();

            var notfoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No clients found", notfoundResult.Value);
        }

        [Fact]
        public async Task GetClient_ReturnsClientDTO()
        {
            var clientId = 1;
            var client = new GetClientDTO 
            { 
                id = clientId 
            };

            _clientServiceMock
                .Setup(s => s.GetClientAsync(clientId))
                .ReturnsAsync(client);

            var actionResult = await _controller.GetClient(clientId);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<GetClientDTO>(okResult.Value);
            Assert.Equal(clientId, response?.id);
        }

        [Fact]
        public async Task GetCLient_ReturnsNotFound_WhenNoClient()
        {
            var clientId = 1;

            _clientServiceMock
                .Setup(s => s.GetClientAsync(clientId))
                .ReturnsAsync((GetClientDTO)null!);

            var actionResult = await _controller.GetClient(clientId);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("Invalid ID. No such client found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetClientsFiltered_ReturnsFilteredClientsWithPagination()
        {
            var testClients = new List<GetClientDTO>
            {
                new GetClientDTO(),
                new GetClientDTO()
            };

            _clientServiceMock
                .Setup(s => s.GetClientsFilteredAsync(It.IsAny<ClientFilterDTO>(), It.IsAny<PaginationDTO>()))
                .ReturnsAsync(testClients);

            var actionResult = await _controller.GetClientsFiltered(It.IsAny<ClientFilterDTO>(), It.IsAny<PaginationDTO>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetClientsFiltered_ReturnsNotFound_WhenNoMatches()
        {
            _clientServiceMock
                .Setup(s => s.GetClientsFilteredAsync(It.IsAny<ClientFilterDTO>(), It.IsAny<PaginationDTO>()))
                .ReturnsAsync(new List<GetClientDTO>());

            var actionResult = await _controller.GetClientsFiltered(It.IsAny<ClientFilterDTO>(), It.IsAny<PaginationDTO>());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal("No clients found for this filter", notFoundResult.Value);
        }

        [Fact]
        public async Task PostClient_ReturnsCreatedClient()
        {

            var expectedClient = new GetClientDTO
            {
                id = 1
            };

            _clientServiceMock
                .Setup(s => s.PostClientAsync(It.IsAny<PostPutClientDTO>()))
                .ReturnsAsync(expectedClient);

            var actionResult = await _controller.PostClient(new PostPutClientDTO());

            Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        }

        [Fact]
        public async Task PutClient_ReturnsNoContent_WhenUpdated()
        {
            var id = 1;

            _clientServiceMock
                .Setup(s => s.PutClientAsync(id, It.IsAny<PostPutClientDTO>()))
                .ReturnsAsync(new GetClientDTO());

            var actionResult = await _controller.PutClient(id, It.IsAny<PostPutClientDTO>());

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task PutClient_ReturnsNotFound_WhenClientNotExists()
        {
            var id = 999;

            _clientServiceMock
                .Setup(s => s.PutClientAsync(id, It.IsAny<PostPutClientDTO>()))
                .ReturnsAsync((GetClientDTO)null!);

            var actionResult = await _controller.PutClient(id, It.IsAny<PostPutClientDTO>());

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("The client was not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNoContent_WhenDeleted()
        {
            var id = 1;

            _clientServiceMock
                .Setup(s => s.DeleteClientAsync(id))
                .ReturnsAsync(true);

            var actionResult = await _controller.DeleteClient(id);

            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNotFound_WhenClientNotExists()
        {
            var id = 999;

            _clientServiceMock
                .Setup(s => s.DeleteClientAsync(id))
                .ReturnsAsync(false);

            var actionResult = await _controller.DeleteClient(id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal("The client was not found", notFoundResult.Value);
        }
    }
}