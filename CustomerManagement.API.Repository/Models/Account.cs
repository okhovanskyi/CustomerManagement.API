namespace CustomerManagement.API.Repository.Models
{
    public class Account
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public Guid AccountNumber { get; set; }
    }
}
