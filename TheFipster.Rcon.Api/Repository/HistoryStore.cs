using LiteDB;
using System.Collections.Generic;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Repository
{
    public class HistoryStore : IHistoryStore
    {
        private readonly ILiteCollection<History> _collection;

        public HistoryStore(IStorageProvider storageProvider)
            => _collection = storageProvider.GetCollection<History>();

        public void Insert(History entry)
            => _collection.Insert(entry);

        public IEnumerable<History> Get(int n = 1)
            => _collection.Query().OrderByDescending(x => x.Timestamp).Limit(n).ToEnumerable();
    }
}
