﻿namespace TheFipster.Rcon.Api.Models.Config
{
    public class RconSettings
    {
        public const string SettingsKey = "Rcon";

        public RconHostSettings Host { get; set; }
    }

    public class RconHostSettings
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
