using LiteDB;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface IStorageProvider
    {
        ILiteCollection<T> GetCollection<T>();
        ILiteCollection<T> GetCollection<T>(string name);
    }
}