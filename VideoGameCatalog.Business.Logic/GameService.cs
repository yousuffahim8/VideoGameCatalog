using VideoGameCatalog.Models;
using VideoGameCatalog.Repository.Repositories.Interfaces;

namespace VideoGameCatalog.Business.Logic
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository repository)
        {
            this.gameRepository = repository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await gameRepository.GetAllAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await gameRepository.GetByIdAsync(id);
        }

        public async Task<Game> AddAsync(Game game)
        {
            return await gameRepository.AddAsync(game);
        }

        public async Task UpdateAsync(Game game)
        {
            await gameRepository.UpdateAsync(game);
        }

        public async Task DeleteAsync(int id)
        {
            await gameRepository.DeleteAsync(id).ConfigureAwait(false);
        }
    }
}


