using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace CustomerManagement.API.Persistence
{
    [ExcludeFromCodeCoverage]
    public class AccountCollection : ConcurrentDictionary<long, Tuple<long, Guid>>
    {
        private int Identity;

        public bool AddAccount(long userId, Guid accountNumber)
        {
            return TryAdd(Interlocked.Increment(ref Identity), new Tuple<long, Guid>(userId, accountNumber));
        }
    }
}
