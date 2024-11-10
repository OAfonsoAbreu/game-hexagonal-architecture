using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService) =>
            _gameService = gameService;

        [HttpGet("/playstation/games")]
        [AllowAnonymous]
        public async Task<IActionResult> InitialsGames()
        {
            return Ok(await _gameService.InitialData());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGames()
        {
            return Ok(await _gameService.getAllGame());
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGame(string name)
        {
            var game = await _gameService.getGameByName(name);

            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateGame(Game newGame)
        {
            bool wasInsert = await _gameService.RegisterGame(newGame);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGame(Game game)
        {
            var existingGame = await _gameService.getGameById(game.Id);

            if (existingGame == null)
            {
                return NotFound();
            }

            existingGame.Name = game.Name;

            bool wasUpdated = await _gameService.UpdateGame(game);

            if (!wasUpdated)
                return StatusCode(500, "Erro interno ao alterar jogo.");

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var existingGame = await _gameService.getGameById(id);

            if (existingGame == null)
            {
                return NotFound();
            }

            bool wasDeleted = await _gameService.DeleteGame(existingGame);

            if (!wasDeleted)
                return StatusCode(500, "Erro interno ao excluir jogo.");

            return Ok();
        }

    }
}
