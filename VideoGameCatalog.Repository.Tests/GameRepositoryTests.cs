using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Models;
using VideoGameCatalog.Repository.DbContext;
using VideoGameCatalog.Repository.Repositories.Implementation;

namespace VideoGameCatalog.Repository.Tests
{
    [TestFixture]
    public class GameRepositoryTests
    {
        private DbContextOptions<AppDbContext> _dbContextOptions;
        private AppDbContext _context;
        private GameRepository _repository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(_dbContextOptions);
            _repository = new GameRepository(_context);
        }

        [Test]
        public async Task AddAsync_AddsGame_ReturnsGame()
        {
            var game = new Game { Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };

            var result = await _repository.AddAsync(game);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Test"));
            Assert.That(_context.Games.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllGames()
        {
            _context.Games.Add(new Game { Title = "Test1", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now });
            _context.Games.Add(new Game { Title = "Test2", Genre = "RPG", Price = 20, ReleaseDate = DateTime.Now });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetByIdAsync_GameExists_ReturnsGame()
        {
            var game = new Game { Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByIdAsync(game.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo(game.Title));
        }

        [Test]
        public async Task GetByIdAsync_GameDoesNotExist_ReturnsNull()
        {
            var result = await _repository.GetByIdAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateAsync_UpdatesGame()
        {
            var game = new Game { Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            game.Title = "Updated";
            await _repository.UpdateAsync(game);

            var updated = await _context.Games.FindAsync(game.Id);
            Assert.That(updated.Title, Is.EqualTo("Updated"));
        }

        [Test]
        public async Task DeleteAsync_DeletesGame()
        {
            var game = new Game { Title = "Test", Genre = "Action", Price = 10, ReleaseDate = DateTime.Now };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync(game.Id);

            var deleted = await _context.Games.FindAsync(game.Id);
            Assert.That(deleted, Is.Null);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}