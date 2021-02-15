using System;

namespace TheFipster.Rcon.Api.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }

        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
