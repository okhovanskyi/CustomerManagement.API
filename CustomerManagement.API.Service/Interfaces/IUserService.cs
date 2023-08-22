using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetUserAsync(Guid customerUid);
    }
}
