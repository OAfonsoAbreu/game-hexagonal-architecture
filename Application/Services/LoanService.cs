using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Game> _gameRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanService(IRepository<User> userRepository, IRepository<Game> gameRepository, ILoanRepository loanRepository) =>
            (_userRepository, _gameRepository, _loanRepository) = (userRepository, gameRepository, loanRepository);

        public async Task<bool> DeleteLoan(Loan loan)
        {
            try
            {
                var loanDB = await _loanRepository.Get(loan.Id);

                if (loanDB == null)
                    return false;

                await _loanRepository.Delete(loanDB.Id);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<Loan>> getAllLoans()
        {
            return await _loanRepository.GetAll();
        }

        public async Task<Loan> getLoanById(int id)
        {
            return await _loanRepository.Get(id);
        }

        public async Task<Loan> getLoanByName(string userName, string gameName)
        {
            return await _loanRepository.Get(userName, gameName);
        }

        public async Task<bool> RegisterLoan(Loan loan)
        {
            try
            {
                var userDB = await _userRepository.Get(loan.User.Name);
                if (userDB == null) return false;

                var gameDb = await _gameRepository.Get(loan.Game.Name);
                if (gameDb == null) return false;

                loan.User = userDB;
                loan.Game = gameDb;

                await _loanRepository.InsertOrUpdate(loan);

                return true;
            }
            catch
            {

                return false;
            }
            
        }

        public async Task<bool> UpdateLoan(Loan loan)
        {
            try
            {
                var userDB = await _userRepository.Get(loan.User.Name);
                if (userDB == null) return false;

                var gameDb = await _gameRepository.Get(loan.Game.Name);
                if (gameDb == null) return false;

                loan.User = userDB;
                loan.Game = gameDb;

                await _loanRepository.InsertOrUpdate(loan);

                return true;
            }
            catch
            {

                return false;
            }
            
        }
    }
}
