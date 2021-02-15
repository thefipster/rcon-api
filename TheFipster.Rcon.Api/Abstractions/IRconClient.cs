using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface IRconClient
    {
        Task<string> ExecuteAsync(string command);
        Task<List<string>> ExecuteAsync(List<string> commands);
    }
}
