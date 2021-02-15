using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheFipster.Rcon.Api.Abstractions;

namespace TheFipster.Rcon.Api.Jobs
{
    public class RconCommandJob : IJob
    {
        public const string CommandsKey = "commands";

        private readonly IRconClient _rconClient;

        public RconCommandJob(IRconClient rconClient)
            => _rconClient = rconClient;

        public async Task Execute(IJobExecutionContext context)
        {
            if (!context.JobDetail.JobDataMap.TryGetValue(CommandsKey, out var commands))
                throw new ArgumentNullException(CommandsKey, "There were no commands in the JobDataMap.");

            if (!(commands is ICollection<string> commandList))
                throw new ArgumentException("The commands value in the JobDataMap is not a valid collection.", CommandsKey);

            await _rconClient.ExecuteAsync(commandList);
        }
    }
}
