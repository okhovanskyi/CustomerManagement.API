namespace CustomerManagement.API.Query.Interfaces
{
    public interface IQueryHandler<in TQuery, TQueryResult>
        where TQuery : IQuery
    {
        public Task<TQueryResult> HandleAsync(TQuery query);
    }
}
