using Domain.Adapters;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;


        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] Account model)
        {
            var account = await _authService.GetAccount(model.Username, model.Password);

            if (account == null || account.Id == 0)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = _authService.GenerateToken(account);
            account.Password = "";
            return new
            {
                account = account,
                token = token
            };
        }

    }
}
