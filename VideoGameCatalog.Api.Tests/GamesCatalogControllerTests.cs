using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoGameCatalog.Api.Controllers;
using VideoGameCatalog.Business;
using VideoGameCatalog.Models;

namespace VideoGameCatalog.Api.Tests
{
    [TestFixture]
    public class GamesCatalogControllerTests
    {
        private Mock<IGameService> _gameServiceMock;
        private GamesCatalogController _controller;

        [SetUp]
        public void Setup()
        {
            _gameServiceMock = new Mock<IGameService>();
            _controller = new GamesCatalogController(_gameServiceMock.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkWithGames()
        {
            var games = new List<Game> { new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now } };
            _gameServiceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(games);

            var result = await _controller.GetAll(It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(games));
        }

        [Test]
        public async Task GetById_GameExists_ReturnsOk()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _gameServiceMock.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var result = await _controller.GetById(1, It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(game));
        }

        [Test]
        public async Task GetById_GameDoesNotExist_ReturnsNotFound()
        {
            _gameServiceMock.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((Game)null);

            var result = await _controller.GetById(1, It.IsAny<CancellationToken>());

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _gameServiceMock.Setup(s => s.AddAsync(game, It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var result = await _controller.Create(game, It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
            var createdResult = result as CreatedAtActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(createdResult?.ActionName, Is.EqualTo("GetById"));
                Assert.That(createdResult?.Value, Is.EqualTo(game));
            });
        }

        [Test]
        public async Task Update_IdMismatch_ReturnsBadRequest()
        {
            var game = new Game { Id = 2, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };

            var result = await _controller.Update(1, game, It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task Update_Valid_ReturnsNoContent()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _gameServiceMock.Setup(s => s.UpdateAsync(game, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.Update(1, game, It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Delete_ReturnsNoContent()
        {
            _gameServiceMock.Setup(s => s.DeleteAsync(1, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1, It.IsAny<CancellationToken>());

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }
    }
}