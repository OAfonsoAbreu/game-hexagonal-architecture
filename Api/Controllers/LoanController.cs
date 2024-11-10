using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService) =>
            _loanService = loanService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoans()
        {
            return Ok(await _loanService.getAllLoans());
        }

        [HttpGet("{userName}/{gameName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLoan(string userName, string gameName)
        {
            var loan = await _loanService.getLoanByName(userName, gameName);

            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateLoan(Loan loan)
        {
            bool wasInsert = await _loanService.RegisterLoan(loan);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateLoan(Loan loan)
        {
            var existingLoan = await _loanService.getLoanById(loan.Id);

            if (existingLoan == null)
            {
                return NotFound();
            }

            existingLoan.Game = loan.Game;
            existingLoan.User = loan.User;

            bool wasUpdated = await _loanService.UpdateLoan(existingLoan);

            if (!wasUpdated)
                return StatusCode(500, "Erro interno ao alterar empréstimo.");

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var existingLoan = await _loanService.getLoanById(id);

            if (existingLoan == null)
            {
                return NotFound();
            }

            bool wasDeleted = await _loanService.DeleteLoan(existingLoan);

            if (!wasDeleted)
                return StatusCode(500, "Erro interno ao excluir empréstimo.");

            return Ok();
        }

    }
}
