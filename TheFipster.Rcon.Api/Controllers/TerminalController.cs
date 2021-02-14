using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;

namespace TheFipster.Rcon.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TerminalController : ControllerBase
    {
        private readonly IRconClient _rconClient;

        public TerminalController(IRconClient rconClient)
        {
            _rconClient = rconClient;
        }

        /// <summary>
        /// Send a command to the game and receive the response.
        /// </summary>
        /// <param name="command">This will be send to the server.</param>
        /// <returns>This is the answer of the server.</returns>
        [HttpPost]
        public async Task<string> PostAsync([FromBody] string command)
            => await _rconClient.ExecuteAsync(command);
    }
}
