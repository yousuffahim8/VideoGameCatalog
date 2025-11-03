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

        public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await gameRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await gameRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Game> AddAsync(Game game, CancellationToken cancellationToken)
        {
            return await gameRepository.AddAsync(game, cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
        {
            await gameRepository.UpdateAsync(game, cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await gameRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        }
    }
}


