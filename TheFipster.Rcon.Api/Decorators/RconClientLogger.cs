using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;

namespace TheFipster.Rcon.Api.Decorators
{
    public class RconClientLogger : IRconClient
    {
        private readonly IRconClient _component;
        private readonly ILogger<RconClientLogger> _logger;

        public RconClientLogger(IRconClient component, ILogger<RconClientLogger> logger)
        {
            _component = component;
            _logger = logger;
        }

        public async Task<string> ExecuteAsync(string command)
        {
            _logger.LogTrace($"Entering IRconClient.ExecuteAsync with Command '{command}'.");
            var result = await _component.ExecuteAsync(command);
            _logger.LogTrace($"Leaving IRconClient.ExecuteAsync with Result '{result}'.");
            return result;
        }
    }
}
