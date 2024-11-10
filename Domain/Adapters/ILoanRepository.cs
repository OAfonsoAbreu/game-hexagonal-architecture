using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
    public interface ILoanRepository : IRepository<Loan>
    {
        Task<Loan> Get(string userName, string gameName);
        Task<bool> InsertOrUpdate(Loan loan);
    }
}
