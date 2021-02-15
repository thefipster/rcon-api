using CoreRCON;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Exceptions;
using TheFipster.Rcon.Api.Models.Config;

namespace TheFipster.Rcon.Api.Services
{
    public class RconClient : IRconClient
    {
        private readonly RconSettings _settings;
        private readonly RCON _client;

        public RconClient(IOptionsMonitor<RconSettings> monitor)
        {
            _settings = monitor.CurrentValue;
            _client = SetupRcon();
        }

        public async Task<string> ExecuteAsync(string command)
        {
            try
            {
                await _client.ConnectAsync();
                return await _client.SendCommandAsync(command);
            }
            catch (SocketException socketEx)
            {
                throw new RconHostException("RCON host is not responding.", socketEx);
            }
        }

        public async Task<List<string>> ExecuteAsync(List<string> commands)
        {
            var results = new List<string>();
            try
            {
                await _client.ConnectAsync();
                foreach (var command in commands)
                {
                    var result = await _client.SendCommandAsync(command);
                    results.Add(result);
                }
            }
            catch (SocketException socketEx)
            {
                throw new RconHostException("RCON host is not responding.", socketEx);
            }

            return results;
        }

        private RCON SetupRcon()
        {
            var address = _settings.Host.Address;

            if (!IPAddress.TryParse(address, out var ipAddress))
            {
                var resolvedAddresses = Dns.GetHostAddresses(_settings.Host.Address);
                if (resolvedAddresses.Length == 0)
                    throw new RconAddressException($"Rcon Address '{address}' couldn't be resolved to an ip address.");

                ipAddress = resolvedAddresses.First();
            }

            return new RCON(ipAddress, (ushort)_settings.Host.Port, _settings.Host.Password);
        }
    }
}
