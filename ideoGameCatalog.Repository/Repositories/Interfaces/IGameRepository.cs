using VideoGameCatalog.Models;

namespace VideoGameCatalog.Repository.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();

        Task<Game?> GetByIdAsync(int id);

        Task<Game> AddAsync(Game game);

        Task UpdateAsync(Game game);

        Task DeleteAsync(int id);
    }
}
