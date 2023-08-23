using CustomerManagement.API.Query.Interfaces;

namespace CustomerManagement.API.Query.Queries
{
    public class GetUserFinancialDataQuery : IQuery
    {
        public GetUserFinancialDataQuery() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Guid CustomerUid { get; set; }
    }
}
