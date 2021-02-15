using LiteDB;
using Microsoft.Extensions.Options;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Models.Config;

namespace TheFipster.Rcon.Api.Repository
{
    public class StorageProvider : IStorageProvider
    {
        private readonly LiteDatabase _database;

        public StorageProvider(IOptionsMonitor<StorageSettings> settings)
            => _database = new LiteDatabase(settings.CurrentValue.File);

        public ILiteCollection<T> GetCollection<T>()
            => _database.GetCollection<T>();

        public ILiteCollection<T> GetCollection<T>(string name)
            => _database.GetCollection<T>(name);
    }
}
