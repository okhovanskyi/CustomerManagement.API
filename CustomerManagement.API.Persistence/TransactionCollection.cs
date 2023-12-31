﻿using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using CustomerManagement.API.Persistence.Enums;

namespace CustomerManagement.API.Persistence
{
    [ExcludeFromCodeCoverage]
    public class TransactionCollection : ConcurrentDictionary<long, Tuple<long, Guid, DateTime, TransactionType>>
    {
        private int Identity;

        public bool AddTransaction(long amount, Guid accountNumber, DateTime createdDateTime, TransactionType transactionType)
        {
            return TryAdd(
                Interlocked.Increment(ref Identity), 
                new Tuple<long, Guid, DateTime, TransactionType>(amount, accountNumber, createdDateTime, transactionType));
        }
    }
}