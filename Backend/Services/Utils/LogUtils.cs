using System;
using System.IO;

namespace Services.Utils
{
    public class LogUtils
    {
        public static void Log(string logDirectory, string message)
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string currentTime = DateTime.Now.ToString("HH-mm-ss");

            string dateFolder = $"{logDirectory}\\LOG_{currentDate}";
            if (!Directory.Exists(dateFolder))
            {
                Directory.CreateDirectory(dateFolder);
            }

            string logFileName = $"LOG_{currentDate}_{currentTime}.txt";
            string logFilePath = $"{dateFolder}\\LOG_{logFileName}";

            File.WriteAllText(logFilePath, $"[{DateTime.Now}] {message}");
        }
    }
}
