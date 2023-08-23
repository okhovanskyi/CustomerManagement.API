namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class AccountBalanceDto
    {
        public Guid AccountNumber { get; set; }

        public long Balance { get; set; }
    }
}
