using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Mappers
{
    internal static class TransactionMapperExtension
    {
        internal static Transaction? FromTransactionDto(this TransactionDto input)
        {
            if (input == null)
            {
                return null;
            }

            return new Transaction
            {
                AccountNumber = input.AccountNumber,
                Amount = input.Amount,
                CreatedDateTime = DateTime.UtcNow,
                TransactionType = input.TransactionType
            };
        }
    }
}
