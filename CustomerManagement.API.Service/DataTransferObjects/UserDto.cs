namespace CustomerManagement.API.Service.DataTransferObjects
{
    public class UserDto
    {
        public Guid CustomerUid { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public long Balance { get; set; }
    }
}
