using VideoGameCatalog.Models;

namespace VideoGameCatalog.Repository.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken);

        Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Game> AddAsync(Game game, CancellationToken cancellationToken);

        Task UpdateAsync(Game game, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
