using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace PBE_NewFileExtractor
{
    public static class LogSettings
    {
        public static Logger CreateLogger()
        {
            return new LoggerConfiguration().MinimumLevel.Override("Microsoft", LogEventLevel.Warning).MinimumLevel.Override("System", LogEventLevel.Warning).MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information).Enrich.FromLogContext().WriteTo.Console(theme: AnsiConsoleTheme.Literate, outputTemplate: "[{Timestamp:G}] [{Level:u3}] {Message:l}{NewLine}").CreateLogger();
        }
    }
}