using System;
using System.IO;
using System.Diagnostics;

namespace LogUtility
{
    /// <summary>
    /// Description of Logger.
    /// </summary>
    public static class Logger
    {
        public static LogLevel AcceptedLogLevel =
#if DEBUG
            LogLevel.Trace
#else
            LogLevel.Info
#endif
            ;
        public static string LogDirectory = "";
        public static string FileName = "";

        public static void Log(LogLevel lvl, string msg)
        {
            string path;
            try
            {
                if (string.IsNullOrWhiteSpace(LogDirectory) || string.IsNullOrWhiteSpace(FileName))
                {
                    LogDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                    if (Directory.Exists(LogDirectory) == false)
                    {
                        Directory.CreateDirectory(LogDirectory);
                    }
                    FileName = "log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                }
                if (lvl > AcceptedLogLevel)
                {
                    return;
                }
                path = Path.Combine(LogDirectory, FileName);
                string datestr = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss.fff");
                string txt = "[" + Enum.GetName(typeof(LogLevel), lvl) + "-" + datestr + "]: " + msg;
                Debug.WriteLine(txt);
                File.AppendAllText(path, txt + Environment.NewLine);
            }
            catch
            {

            }
        }

        public static void Log(LogLevel lvl, Exception ex)
        {
            Log(lvl, ex.ToString());
        }

        public static void Log(Exception ex)
        {
            Log(LogLevel.Error, ex.ToString());
        }
    }

    public enum LogLevel
    {
        Warning,
        Error,
        Info,
        Debug,
        Trace,
    }
}