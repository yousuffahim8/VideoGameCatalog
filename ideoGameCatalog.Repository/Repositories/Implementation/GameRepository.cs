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

        public async Task<IEnumerable<Game>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await context.Game.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Game.FindAsync(id, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Game> AddAsync(Game game, CancellationToken cancellationToken)
        {
            context.Game.Add(game);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return game;
        }

        public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
        {
            var existingGame = await context.Game.FindAsync(game.Id).ConfigureAwait(false);
            if (existingGame == null)
                return;

            if (existingGame.Title != game.Title)
                existingGame.Title = game.Title;

            if (existingGame.Genre != game.Genre)
                existingGame.Genre = game.Genre;

            if (existingGame.Price != game.Price)
                existingGame.Price = game.Price;

            if (existingGame.ReleaseDate != game.ReleaseDate)
                existingGame.ReleaseDate = game.ReleaseDate;

            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var game = await GetByIdAsync(id, cancellationToken);
            if (game != null)
            {
                context.Game.Remove(game);
                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }

}
