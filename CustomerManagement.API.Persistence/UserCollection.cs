using System.Collections.Concurrent;

namespace CustomerManagement.API.Persistence
{
    public class UserCollection : ConcurrentDictionary<long, Tuple<Guid, string, string>>
    {
        private const string NameString = "Name";
        private const string SurnameString = "Surname";

        private int Identity;

        public UserCollection(short AmountOfUsers)
        {
            bool usersBeingAddedSuccesfully = true;

            while (usersBeingAddedSuccesfully && Identity <= AmountOfUsers)
            {
                usersBeingAddedSuccesfully = AddUser(Guid.NewGuid(), $"{NameString}{Identity}", $"{SurnameString}{Identity}");
            }
        }
        
        public bool AddUser(Guid customerUid, string Name, string Surname)
        {
            return TryAdd(Interlocked.Increment(ref Identity), new Tuple<Guid, string, string>(customerUid, Name, Surname));
        }
    }
}
