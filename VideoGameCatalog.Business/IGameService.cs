using VideoGameCatalog.Models;

namespace VideoGameCatalog.Business
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken);

        Task<Game> AddAsync(Game game, CancellationToken cancellationToken);

        Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task UpdateAsync(Game game, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
