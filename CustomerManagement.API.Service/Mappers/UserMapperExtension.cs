using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Mappers
{
    internal static class UserMapperExtension
    {
        internal static UserDto? ToUserDto(this User input)
        {
            if (input == null)
            {
                return null;
            }

            return new UserDto
            {
                CustomerUid = input.CustomerUid,
                Name = input.Name,
                Surname = input.Surname
            };
        }
    }
}
