using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository) =>
            _gameRepository = gameRepository;

        public async Task<bool> DeleteGame(Game game)
        {
            return await _gameRepository.Delete(game.Id);
        }

        public async Task<IEnumerable<Game>> getAllGame()
        {
            return await _gameRepository.GetAll();
        }

        public async Task<Game> getGameById(int id)
        {
            return await _gameRepository.Get(id);
        }

        public async Task<Game> getGameByName(string name)
        {
            return await _gameRepository.Get(name);
        }

        public Task<bool> InitialData()
        {
            Util.InitialDataGame util = new Util.InitialDataGame(_gameRepository);
            util.InsertData("Call of Duty")
                .InsertData("God of War")
                .InsertData("Assassin's Creed")
                .InsertData("GTA 5")
                .InsertData("FIFA 2024");

            return Task.FromResult(true);
        }

        public async Task<bool> RegisterGame(Game game)
        {
            return await _gameRepository.Insert(game);
        }

        public async Task<bool> UpdateGame(Game game)
        {
            return await _gameRepository.Update(game);
        }
    }
}
