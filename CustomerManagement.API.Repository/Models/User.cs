namespace CustomerManagement.API.Repository.Models
{
    public class User
    {
        public long Id { get; set; }

        public Guid CustomerUid { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }
    }
}
