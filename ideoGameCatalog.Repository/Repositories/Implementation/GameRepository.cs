using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Models;
using VideoGameCatalog.Repository.DbContext;
using VideoGameCatalog.Repository.Repositories.Interfaces;

namespace VideoGameCatalog.Repository.Repositories.Implementation
{

    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext context;

        public GameRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
           return await context.Game.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await context.Game.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<Game> AddAsync(Game game)
        {
            context.Game.Add(game);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return game;
        }

        public async Task UpdateAsync(Game game)
        {
            context.Entry(game).State = EntityState.Modified;
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            var game = await GetByIdAsync(id);
            if (game != null)
            {
                context.Game.Remove(game);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }

}
