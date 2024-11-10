using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IGameService
    {
        Task<Game> getGameByName(string name);
        Task<Game> getGameById(int id);
        Task<IEnumerable<Game>> getAllGame();
        Task<bool> RegisterGame(Game game);
        Task<bool> UpdateGame(Game game);
        Task<bool> DeleteGame(Game game);
        Task<bool> InitialData();
    }
}
