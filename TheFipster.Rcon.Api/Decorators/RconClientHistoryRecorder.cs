using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Decorators
{
    public class RconClientHistoryRecorder : IRconClient
    {
        private readonly IRconClient _component;
        private readonly IHistoryStore _store;

        public RconClientHistoryRecorder(IRconClient component, IHistoryStore store)
        {
            _component = component;
            _store = store;
        }

        public async Task<string> ExecuteAsync(string command)
        {
            var result = await _component.ExecuteAsync(command);

            var entry = new History(command, result);
            _store.Insert(entry);

            return result;
        }

        public async Task<List<string>> ExecuteAsync(List<string> commands)
        {
            var results = await _component.ExecuteAsync(commands);

            for (int i = 0; i < commands.Count(); i++)
            {
                var entry = new History(commands[i], results[i]);
                _store.Insert(entry);
            }

            return results;
        }
    }
}
