using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace TaskManager.API.Extensions
{
    public class LogSettings
    {
        public static void SetUpSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(
                    path: ".\\Logs\\log-.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                )
                .Enrich.WithExceptionDetails()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .CreateLogger();
        }
    }
}
