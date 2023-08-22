using CustomerManagement.API.Repository.Interfaces;
using CustomerManagement.API.Service.DataTransferObjects;
using CustomerManagement.API.Service.Interfaces;
using CustomerManagement.API.Service.Mappers;

namespace CustomerManagement.API.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetUserAsync(Guid customerUid)
        {
            var user = await _userRepository.GetUserAsync(customerUid);

            return user.ToUserDto();
        }
    }
}
