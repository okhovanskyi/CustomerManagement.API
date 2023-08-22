namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class UserDto
    {
        public long UserId { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public long Balance { get; set; }
    }
}
