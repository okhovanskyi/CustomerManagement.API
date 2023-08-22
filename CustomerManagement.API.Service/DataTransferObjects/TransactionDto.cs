using CustomerManagement.API.Repository.Models.Enums;

namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class TransactionDto
    {
        public long Amount { get; set; }

        public Guid AccountNumber { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
