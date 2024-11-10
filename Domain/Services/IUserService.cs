using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IUserService
    {
        Task<User> getUserByName(string name);
        Task<User> getUserById(int id);
        Task<IEnumerable<User>> getAllUser();
        Task<bool> RegisterUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteAUser(User user);
        Task<bool> InitialData();
    }
}
