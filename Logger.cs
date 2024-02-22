using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddIn_Assignment
{
    /// <summary>
    /// Logger class for logging exceptions and errors to a text file with timestamps.
    /// </summary>
    public static class Logger
    {
        private static readonly string logFilePath = @"../../Logs/error_log.txt";

        /// <summary>
        /// Logs an exception with timestamp to the text file.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(Exception ex)
        {
            Log($"[EXCEPTION] {DateTime.Now}: {ex.ToString()}");
        }

        /// <summary>
        /// Logs an error message with timestamp to the text file.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public static void LogError(string message)
        {
            Log($"[ERROR] {DateTime.Now}: {message}");
        }

        /// <summary>
        /// Logs a message with timestamp to the text file.
        /// </summary>
        /// <param name="message">The message to log.</param>
        private static void Log(string message)
        {
            try
            {
                // Ensure the directory exists
                string directory = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Write to the log file
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine("\n"+ message +"\n");
                }
            }
            catch (Exception ex)
            {
                // If logging fails, write to console
                Console.WriteLine($"Failed to log message: {ex.Message}");
            }
        }
    }
}
