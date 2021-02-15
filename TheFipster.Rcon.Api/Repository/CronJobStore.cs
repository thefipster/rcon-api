using LiteDB;
using System.Collections.Generic;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Repository
{
    public class CronJobStore : ICronJobStore
    {
        private readonly ILiteCollection<CronJob> _collection;

        public CronJobStore(IStorageProvider storageProvider)
            => _collection = storageProvider.GetCollection<CronJob>();

        public IEnumerable<CronJob> Get()
            => _collection.FindAll();

        public CronJob Get(string id)
            => _collection.FindById(new BsonValue(id));

        public string Insert(CronJob job)
            => _collection.Insert(job).AsString;

        public void Delete(string id)
            => _collection.Delete(new BsonValue(id));
    }
}
