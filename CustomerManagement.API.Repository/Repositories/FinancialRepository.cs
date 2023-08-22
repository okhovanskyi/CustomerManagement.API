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
        public Task CreateTransactionAsync(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAccountBalanceAsync(UserAccountBalance userAccountBalance)
        {
            throw new NotImplementedException();
        }

        public Task GetUserAccountsBalanceAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAccountBalanceAsync(UserAccountBalance userAccountBalance)
        {
            throw new NotImplementedException();
        }
    }
}
