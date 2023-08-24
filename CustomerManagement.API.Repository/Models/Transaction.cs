using CustomerManagement.API.Persistence.Enums;

namespace CustomerManagement.API.Repository.Models
{
    public class Transaction
    {
        public long Id { get; set; }

        public long Amount { get; set; }

        public Guid AccountNumber { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public TransactionType TransactionType { get; set; }  
    }
}
