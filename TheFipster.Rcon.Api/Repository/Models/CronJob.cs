using System;
using System.Collections.Generic;

namespace TheFipster.Rcon.Api.Repository.Models
{
    public class CronJob
    {
        public CronJob()
        {

        }

        public CronJob(string cronExpression, ICollection<string> commands)
        {
            Id = Guid.NewGuid().ToString();
            CronExpression = cronExpression;
            Commands = commands;
        }

        public string Id { get; set; }
        public ICollection<string> Commands { get; set; }
        public string CronExpression { get; set; }
    }
}
