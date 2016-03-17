using System;
using System.Text;

namespace Trinity
{
    public interface INeoConnectionConfig
    {
        bool IsCypherLoggingEnabled { get; set; }

        string GraphUri { get; }

        string Username { get; }

        string Password { get; }

        string ProxyUri { get; }
    }

    public static class INeoConnectionConfigExtensions
    {
        public static bool HasProxyUri(this INeoConnectionConfig config)
        {
            return !string.IsNullOrWhiteSpace(config.ProxyUri);
        }
        public static string ToStringX(this INeoConnectionConfig config)
        {
            var sb = new StringBuilder();
            sb.Append($"GraphUri={config.GraphUri}, Username='{config.Username}', ProxyUri='{config.ProxyUri}'");
            return sb.ToString();
        }
    }

    public class NeoConnectionConfig : INeoConnectionConfig
    {
        public bool IsCypherLoggingEnabled { get; set; }
        public string GraphUri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProxyUri { get; set; }
    }
}
