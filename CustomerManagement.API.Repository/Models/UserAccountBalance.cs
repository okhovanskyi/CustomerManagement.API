namespace CustomerManagement.API.Repository.Models
{
    public class UserAccountBalance
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public Guid AccountNumber { get; set; }

        public long Balance { get; set; }
    }
}
