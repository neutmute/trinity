using System;

namespace Trinity
{
    public interface INeoConnectionConfig
    {
        bool IsCypherLoggingEnabled { get; set; }
        string GraphDbConnectionString { get; }

        string Username { get; }
        string Password { get; }

        int HttpClientTimeoutMilliseconds { get; }
    }

    public class NeoConnectionConfig : INeoConnectionConfig
    {
        public bool IsCypherLoggingEnabled { get; set; }
        public string GraphDbConnectionString { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int HttpClientTimeoutMilliseconds { get; set; }
    }
}
