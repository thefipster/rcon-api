using LiteDB;
using System;

namespace TheFipster.Rcon.Api.Repository.Models
{
    public class History
    {
        public History()
        {

        }

        public History(string command, string result)
        {
            Timestamp = DateTime.UtcNow;
            Command = command;
            Result = result;
        }

        [BsonId]
        public DateTime Timestamp { get; set; }
        public string Command { get; set; }
        public string Result { get; set; }
    }
}
