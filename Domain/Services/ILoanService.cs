using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoanService
    {
        Task<Loan> getLoanById(int id);
        Task<Loan> getLoanByName(string userName, string gameName);
        Task<IEnumerable<Loan>> getAllLoans();
        Task<bool> RegisterLoan(Loan loan);
        Task<bool> UpdateLoan(Loan loan);
        Task<bool> DeleteLoan(Loan loan);
    }
}
