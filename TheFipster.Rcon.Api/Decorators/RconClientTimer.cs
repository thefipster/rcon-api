using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;

namespace TheFipster.Rcon.Api.Decorators
{
    public class RconClientTimer : IRconClient
    {
        private readonly IRconClient _component;
        private readonly ILogger<RconClientTimer> _logger;

        public RconClientTimer(IRconClient component, ILogger<RconClientTimer> logger)
        {
            _component = component;
            _logger = logger;
        }

        public async Task<string> ExecuteAsync(string command)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await _component.ExecuteAsync(command);
            stopwatch.Stop();
            _logger.LogTrace($"IRconClient.ExecuteAsync took {stopwatch.ElapsedMilliseconds} ms.");
            return result;
        }

        public async Task<List<string>> ExecuteAsync(List<string> commands)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = await _component.ExecuteAsync(commands);
            stopwatch.Stop();
            _logger.LogTrace($"IRconClient.ExecuteAsync took {stopwatch.ElapsedMilliseconds} ms.");
            return result;
        }
    }
}
