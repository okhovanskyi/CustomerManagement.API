using CustomerManagement.API.Persistence;
using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountCollection _accountCollection;

        public AccountRepository(AccountCollection accountCollection)
        {
            _accountCollection = accountCollection;
        }

        public async Task<Account?> CreateUserAccountAsync(long userId)
        {
            return await Task.Run(() => 
            { 
                var guid = Guid.NewGuid();

                if (_accountCollection.AddAccount(userId, guid))
                {
                    var account = _accountCollection.FirstOrDefault(pair => pair.Value.Item2.Equals(guid));

                    return new Account
                    {
                        Id = account.Key,
                        UserId = account.Value.Item1,
                        AccountNumber = account.Value.Item2                        
                    };
                }

                return null;
            });            
        }

        public async Task<List<Account>> GetAccountsAsync(long userId)
        {
            return await Task.Run(() =>
            {
                return _accountCollection.Values
                .Where(value => value != null && value.Item1.Equals(userId))
                .Select(value => new Account { AccountNumber = value.Item2})
                .ToList();
            });
        }
    }
}
