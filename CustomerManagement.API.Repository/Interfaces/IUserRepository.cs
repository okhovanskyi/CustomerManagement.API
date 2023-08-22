using CustomerManagement.API.Repository.Models;

namespace CustomerManagement.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid customerUid);
    }
}
