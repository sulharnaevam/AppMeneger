using System;
using System.IO;

namespace AppMeneger 
{ 
    public static class Logger
    {
        private static string logFile = "log.txt";
    
        public static void Log(string message)
        {
            try
            {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
            File.AppendAllText(logFile, logEntry + Environment.NewLine);
            }
            catch { }
        }
    }
}