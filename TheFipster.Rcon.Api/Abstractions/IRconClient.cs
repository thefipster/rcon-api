using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface IRconClient
    {
        Task<string> ExecuteAsync(string command);
        Task<ICollection<string>> ExecuteAsync(ICollection<string> commands);
    }
}
