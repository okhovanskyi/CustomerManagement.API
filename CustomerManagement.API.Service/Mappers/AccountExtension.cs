using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Mappers
{
    internal static class AccountExtension
    {
        internal static UserAccountDto? ToUserAccountDto(this Account input)
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

        internal static AccountBalanceDto? ToAccountBalanceDto(this Account input)
        {
            if (input == null)
            {
                return null;
            }

            return new AccountBalanceDto
            {
                AccountNumber = input.AccountNumber                
            };
        }
    }
}
