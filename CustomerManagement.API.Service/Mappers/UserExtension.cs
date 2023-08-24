using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Mappers
{
    internal static class UserExtension
    {
        internal static UserDto? ToUserDto(this User input)
        {
            if (input == null)
            {
                return null;
            }

            return new UserDto
            {
                UserId = input.Id,
                Name = input.Name,
                Surname = input.Surname
            };
        }
    }
}
