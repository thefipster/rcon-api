using System.Collections.Generic;

namespace TheFipster.Rcon.Api.Models
{
    public class NewJobRequest
    {
        public ICollection<string> Commands { get; set; }
        public string CronExpression { get; set; }
    }
}
