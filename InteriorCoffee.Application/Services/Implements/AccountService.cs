using InteriorCoffee.Application.Services.Interfaces;
using InteriorCoffee.Domain.Models;
using InteriorCoffee.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InteriorCoffee.Application.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            try
            {
                return await _accountRepository.GetAccountList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all accounts.");
                throw;
            }
        }

        public async Task<Account> GetAccountByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid account ID.");
                throw new ArgumentException("Account ID cannot be null or empty.");
            }

            try
            {
                return await _accountRepository.GetAccountById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting account with id {id}.");
                throw;
            }
        }

        public async Task CreateAccountAsync(Account account)
        {
            if (account == null)
            {
                _logger.LogWarning("Invalid account data.");
                throw new ArgumentException("Account cannot be null.");
            }

            try
            {
                await _accountRepository.CreateAccount(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating an account.");
                throw;
            }
        }

        public async Task UpdateAccountAsync(string id, Account account)
        {
            if (string.IsNullOrEmpty(id) || account == null)
            {
                _logger.LogWarning("Invalid account ID or data.");
                throw new ArgumentException("Account ID and data cannot be null or empty.");
            }

            try
            {
                await _accountRepository.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating account with id {id}.");
                throw;
            }
        }

        public async Task DeleteAccountAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid account ID.");
                throw new ArgumentException("Account ID cannot be null or empty.");
            }

            try
            {
                await _accountRepository.DeleteAccount(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting account with id {id}.");
                throw;
            }
        }
    }
}
