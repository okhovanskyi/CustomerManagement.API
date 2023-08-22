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
                UserId = input.Id,
                Name = input.Name,
                Surname = input.Surname
            };
        }

        internal static UserAccountDto? ToUserAccountDto(this UserAccountBalance input)
        {
            if (input == null) 
            { 
                return null; 
            }

            return new UserAccountDto
            {
                UserId = input.UserId,
                AccountNumber = input.AccountNumber
            };
        }
    }
}
