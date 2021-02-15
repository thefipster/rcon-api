using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Controllers
{
    [ApiController]
    [Route("terminal")]
    public class TerminalController : ControllerBase
    {
        private readonly IRconClient _rconClient;
        private readonly IHistoryStore _historyStore;

        public TerminalController(IRconClient rconClient, IHistoryStore historyStore)
        {
            _rconClient = rconClient;
            _historyStore = historyStore;
        }

        /// <summary>
        /// Returns the last command executed to the server.
        /// </summary>
        /// <returns>Latest issued command and result</returns>
        [HttpGet]
        public History GetHistory()
            => _historyStore.Get().FirstOrDefault();

        /// <summary>
        /// Send a command to the game and receive the response.
        /// </summary>
        /// <param name="command">This command will be send to the server.</param>
        /// <returns>This is the answer of the server.</returns>
        [HttpPost]
        public async Task<string> PostAsync([FromBody] string command)
            => await _rconClient.ExecuteAsync(command);

        /// <summary>
        /// Send a command to the game and receive the response.
        /// </summary>
        /// <param name="commands">These commands will be send to the server and executed sequentially.</param>
        /// <returns>These are the answers of the server in the same order as they were sent.</returns>
        [HttpPost("bulk")]
        public async Task<ICollection<string>> PostBulkAsync([FromBody] ICollection<string> commands)
            => await _rconClient.ExecuteAsync(commands);
    }
}
