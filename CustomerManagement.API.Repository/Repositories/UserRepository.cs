using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.API.Repository.Repositories
{
    internal class UserRepository : IUserRepository
    {
        public Task<User> GetUserAsync(Guid customerUid)
        {
            throw new NotImplementedException();
        }
    }
}
