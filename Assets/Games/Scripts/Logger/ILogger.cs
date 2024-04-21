using System.Collections;
using System.Collections.Generic;
using Unity.Logging;
using UnityEngine;

namespace Baruah.Logger
{
    public interface ILogger
    {
        void Print(string message, params object[] param);
        void Warning(string message, params object[] param);
        void Error(string message, params object[] param);
    }

    public static class Log
    {
        private static ILogger instance;

        public static void SetLogger(ILogger logger)
        {
            instance = logger;
        }

        public static void Print(string message, params object[] param)
        {
            instance.Print(message, param);
        }

        public static void Warning(string message, params object[] param)
        {
            instance.Warning(message, param);
        }

        public static void Error(string message, params object[] param)
        {
            instance.Error(message, param);
        }
    }
}
