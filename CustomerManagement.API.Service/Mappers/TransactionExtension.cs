using CustomerManagement.API.Repository.Models;
using CustomerManagement.API.Service.DataTransferObjects;

namespace CustomerManagement.API.Service.Mappers
{
    internal static class TransactionExtension
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

        internal static TransactionDto? ToTransactionDto(this Transaction input)
        {
            if (input == null)
            {
                return null;
            }

            return new TransactionDto
            {
                AccountNumber = input.AccountNumber,
                Amount = input.Amount,
                CreatedDateTime = input.CreatedDateTime,
                TransactionType = input.TransactionType
            };
        }
    }
}
