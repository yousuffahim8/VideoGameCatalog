using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Business;
using VideoGameCatalog.Models;

namespace VideoGameCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesCatalogController : ControllerBase
    {
        private readonly IGameService gameService;

        public GamesCatalogController(IGameService repository)
        {
            this.gameService = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await gameService.GetAllAsync(cancellationToken).ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var game = await gameService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
            return game == null ? NotFound() : Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game, CancellationToken cancellationToken)
        {
            var created = await gameService.AddAsync(game, cancellationToken).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game, CancellationToken cancellationToken)
        {
            if (id != game.Id) return BadRequest();
            await gameService.UpdateAsync(game, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await gameService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}
