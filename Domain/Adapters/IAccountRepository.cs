using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface IAccountRepository
    {
        Task<Account> Get(string userName, string password);
    }
}
