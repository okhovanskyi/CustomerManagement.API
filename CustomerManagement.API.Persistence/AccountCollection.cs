using System.Collections.Concurrent;

namespace CustomerManagement.API.Persistence
{
    public class AccountCollection : ConcurrentDictionary<long, Tuple<long, Guid>>
    {
        private int Identity;

        public bool AddAccount(long userId, Guid accountNumber)
        {
            return TryAdd(Interlocked.Increment(ref Identity), new Tuple<long, Guid>(userId, accountNumber));
        }
    }
}
