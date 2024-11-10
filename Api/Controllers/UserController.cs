using Application.Services;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;

        [HttpGet("/user/users")]
        [AllowAnonymous]
        public async Task<IActionResult> InitialsGames()
        {
            return Ok(await _userService.InitialData());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.getAllUser());
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _userService.getUserByName(name);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUser(User newUser)
        {
            bool wasInsert = await _userService.RegisterUser(newUser);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var existingUser = await _userService.getUserById(user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;

            bool wasUpdated = await _userService.UpdateUser(user);

            if (!wasUpdated)
                return StatusCode(500, "Erro interno ao alterar usuário.");

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userService.getUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            bool wasDeleted = await _userService.DeleteAUser(existingUser);

            if (!wasDeleted)
                return StatusCode(500, "Erro interno ao excluir usuário.");

            return Ok();
        }
    }
}

