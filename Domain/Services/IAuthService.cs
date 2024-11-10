using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAuthService
    {
        public string GenerateToken(Account account);

        public Task<Account> GetAccount(string userName, string password);
    }
}
