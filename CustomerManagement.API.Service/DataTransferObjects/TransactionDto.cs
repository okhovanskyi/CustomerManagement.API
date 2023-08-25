using CustomerManagement.API.Persistence.Enums;

namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class TransactionDto
    {
        public long Amount { get; set; }

        public Guid AccountNumber { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public TransactionType TransactionType { get; set; }

        public string TransactionTypeName => TransactionType.ToString();
    }
}
