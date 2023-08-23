using CustomerManagement.API.Service.DataTransferObjects;
using System.Net;

namespace CustomerManagement.API.Query.QueryResults
{
    public class GetUserFinancialDataQueryResult
    {
        public UserDto? UserDto { get; set; }

        public List<TransactionDto?>? TransactionDtos { get; set; }

        public List<AccountBalanceDto?>? AccountBalanceDtos { get; set; } 

        public HttpStatusCode HttpStatusCode { get; set; }

        public string? Message { get; set; }

        public AggregateException? AggregateException { get; set; }
    }
}
