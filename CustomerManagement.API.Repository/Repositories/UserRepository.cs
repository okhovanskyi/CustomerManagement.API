using CustomerManagement.API.Persistence;
using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserCollection _userCollection;

        public UserRepository(UserCollection userCollection) 
        {
            _userCollection = userCollection;
        }

        public async Task<User?> GetUserAsync(Guid customerUid)
        {
            return await Task.Run(() =>
            {
                var user = _userCollection.FirstOrDefault(pair => pair.Value.Item1.Equals(customerUid));

                if (user.Value == null)
                {
                    return null;
                }

                return new User
                {
                    Id = user.Key,
                    CustomerUid = user.Value.Item1,
                    Name = user.Value.Item2,
                    Surname = user.Value.Item3
                };
            });            
        }
    }
}
