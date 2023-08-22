using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Repository.Repositories
{
    public class FinancialRepository : IFinancialRepository
    {
        public Task CreateTransactionAsync(Transaction? transaction)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccountBalance> CreateUserAccountAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccountBalance> GetUserAccountsBalanceAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccountBalance> UpdateUserAccountBalanceAsync(UserAccountBalance userAccountBalance)
        {
            throw new NotImplementedException();
        }
    }
}
