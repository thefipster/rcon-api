using System.Threading.Tasks;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface IRconClient
    {
        Task<string> ExecuteAsync(string command);
    }
}
