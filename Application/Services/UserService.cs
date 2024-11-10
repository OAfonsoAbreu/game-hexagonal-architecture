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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository) =>
            _userRepository = userRepository;

        public async Task<bool> DeleteAUser(User user)
        {
            return await _userRepository.Delete(user.Id);
        }

        public async Task<IEnumerable<User>> getAllUser()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> getUserById(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<User> getUserByName(string name)
        {
            return await _userRepository.Get(name);
        }

        public Task<bool> InitialData()
        {
            Util.InitialDataUser util = new Util.InitialDataUser(_userRepository);
            util.InsertData("Afonso")
                .InsertData("Joao")
                .InsertData("Maria")
                .InsertData("Pedro")
                .InsertData("Ana");

            return Task.FromResult(true);
            
        }

        public async Task<bool> RegisterUser(User user)
        {
            return await _userRepository.Insert(user);
        }

        public async Task<bool> UpdateUser(User user)
        {
            return await _userRepository.Update(user);
        }
    }
}
