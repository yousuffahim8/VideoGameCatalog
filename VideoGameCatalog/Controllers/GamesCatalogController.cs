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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await gameService.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var game = await gameService.GetByIdAsync(id).ConfigureAwait(false);
            return game == null ? NotFound() : Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            var created = await gameService.AddAsync(game).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            if (id != game.Id) return BadRequest();
            await gameService.UpdateAsync(game).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await gameService.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
