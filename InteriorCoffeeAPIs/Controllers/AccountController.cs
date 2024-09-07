using InteriorCoffee.Application.Services.Interfaces;
using InteriorCoffee.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteriorCoffeeAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        // GET: api/Account/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(string id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        // POST: api/Account
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            // Ensure the _id is not set
            account._id = null;

            await _accountService.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account._id }, account);
        }

        // PUT: api/Account/{id}
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateAccount(string id, [FromBody] Account account)
        {
            if (id != account._id)
            {
                return BadRequest();
            }

            var existingAccount = await _accountService.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _accountService.UpdateAccountAsync(id, account);
            return NoContent();
        }

        // DELETE: api/Account/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
