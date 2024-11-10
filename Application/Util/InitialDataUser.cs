using Domain.Adapters;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Util
{
    public class InitialDataUser
    {
        private IRepository<User> _userRepository;

        public InitialDataUser(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            this.Task = Task.FromResult<int>(0);
        }
            
        public Task Task { get; private set; }

        public InitialDataUser InsertData(string name)
        {
            QueueWork(() => InsertAUser(name));

            return this;
        }

        private async void InsertAUser(string name)
        {
            var userDB = await _userRepository.Get(name);

            if (userDB == null)
            {
                User user = new User() { Name = name };
                await _userRepository.Insert(user);
            }
        }

        private void QueueWork(Action work)
        {
            // queue up the work
            this.Task = this.Task.ContinueWith<InitialDataUser>(task =>
            {
                work();
                return this;
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
