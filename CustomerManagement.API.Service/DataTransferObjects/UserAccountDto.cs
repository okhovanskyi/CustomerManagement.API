using System;
namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class UserAccountDto
    {
        public long UserId { get; set; }

        public Guid AccountNumber { get; set; }        
    }
}
