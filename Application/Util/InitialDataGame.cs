using Domain.Adapters;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Util
{
    public class InitialDataGame
    {
        private IRepository<Game> _gameRepository;

        public InitialDataGame(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
            this.Task = Task.FromResult<int>(0);
        }

        public Task Task { get; private set; }

        public InitialDataGame InsertData(string name)
        {
            QueueWork(() => InsertAGame(name));

            return this;
        }

        private async void InsertAGame(string name)
        {
            var gameDB = await _gameRepository.Get(name);

            if (gameDB == null)
            {
                Game game = new Game() { Name = name };
                await _gameRepository.Insert(game);
            }
        }

        private void QueueWork(Action work)
        {
            // queue up the work
            this.Task = this.Task.ContinueWith<InitialDataGame>(task =>
            {
                work();
                return this;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Task.WaitAll(this.Task);
        }

    }
}
