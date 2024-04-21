using Unity.Logging;
using Unity.Logging.Sinks;

namespace Baruah.Logger
{
    public class UnityLogger : ILogger
    {
        public UnityLogger(string logFile)
        {
            Unity.Logging.Log.Logger = new Unity.Logging.Logger(new Unity.Logging.LoggerConfig()
                .MinimumLevel.Debug()
                .RedirectUnityLogs()
                .OutputTemplate("{Timestamp} - {Level} - {Message}\n{Stacktrace}")
                .WriteTo.File(logFile, minLevel: LogLevel.Verbose)
                .WriteTo.StdOut(outputTemplate: "{Level} || {Timestamp} || {Message} || {Stacktrace}")
                .WriteTo.UnityEditorConsole()
                .WriteTo.StdOut(outputTemplate: "{Level} || {Timestamp} || {Message} || {Stacktrace}")
            );
        }

        public void Error(string message, params object[] param)
        {
            Unity.Logging.Log.Error(message, param);
        }

        public void Print(string message, params object[] param)
        {
            Unity.Logging.Log.Info(message, param);
        }

        public void Warning(string message, params object[] param)
        {
            Unity.Logging.Log.Warning(message, param);
        }
    }
}
