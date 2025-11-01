using Moq;
using VideoGameCatalog.Models;
using VideoGameCatalog.Repository.Repositories.Interfaces;

namespace VideoGameCatalog.Business.Logic.Tests
{
    [TestFixture]
    public class GameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private GameService _gameService;

        [SetUp]
        public void Setup()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _gameService = new GameService(_gameRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllGames()
        {
            var games = new List<Game>
            {
                new Game { Id = 1, Title = "Test1", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now },
                new Game { Id = 2, Title = "Test2", Genre = "RPG", Price = 20, ReleaseDate = DateTime.Now }
            };
            _gameRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(games);

            var result = await _gameService.GetAllAsync();

            Assert.That(result, Is.EqualTo(games));
        }

        [Test]
        public async Task GetByIdAsync_GameExists_ReturnsGame()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(game);

            var result = await _gameService.GetByIdAsync(1);

            Assert.That(result, Is.EqualTo(game));
        }

        [Test]
        public async Task GetByIdAsync_GameDoesNotExist_ReturnsNull()
        {
            _gameRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Game)null);

            var result = await _gameService.GetByIdAsync(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddAsync_AddsGame_ReturnsAddedGame()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _gameRepositoryMock.Setup(r => r.AddAsync(game)).ReturnsAsync(game);

            var result = await _gameService.AddAsync(game);

            Assert.That(result, Is.EqualTo(game));
        }

        [Test]
        public async Task UpdateAsync_UpdatesGame_CallsRepository()
        {
            var game = new Game { Id = 1, Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };

            await _gameService.UpdateAsync(game);

            _gameRepositoryMock.Verify(r => r.UpdateAsync(game), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesGame_CallsRepository()
        {
            await _gameService.DeleteAsync(1);

            _gameRepositoryMock.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}