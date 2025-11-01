using VideoGameCatalog.Models;

namespace VideoGameCatalog.Business
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllAsync();

        Task<Game> AddAsync(Game game);

        Task<Game?> GetByIdAsync(int id);

        Task UpdateAsync(Game game);

        Task DeleteAsync(int id);

    }
}
