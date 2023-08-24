using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace CustomerManagement.API.Persistence
{
    [ExcludeFromCodeCoverage]
    public class UserCollection : ConcurrentDictionary<long, Tuple<Guid, string, string>>
    {
        private int Identity;

        public bool AddUser(Guid customerUid, string Name, string Surname)
        {
            return TryAdd(Interlocked.Increment(ref Identity), new Tuple<Guid, string, string>(customerUid, Name, Surname));
        }
    }
}
