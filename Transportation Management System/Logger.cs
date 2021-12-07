
/* -- FILEHEADER COMMENT --
    FILE		:	Logger.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Logger class.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;
using System.IO;


namespace Transportation_Management_System
{
    /// <summary>
    /// Enum to convert LogLevel into log titles.
    /// </summary>
    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }

    /// 
    /// \class Logger
    /// 
    /// \brief The purpose of this class is to model the logger class
    ///
    /// This class will demonstrate the attributes of a Logger Class, with the main attribute being the logDirectory.
    /// It will contain two methods to write to a log file and change/update the directory that contains the log file.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Logger
    {
        // Current directory is the default one

        private static bool isSetup = false;

        /// 
        /// \brief Set up the LogClass if it hasn't been setup already
        /// 
        private static void Setup()
        {
            string fileName = string.Empty;
            string logDirectory = string.Empty;
            try
            {
                // Load config
                fileName = ConfigurationManager.AppSettings.Get("LogFileName");
                logDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");
                // If directory is empty
                if (logDirectory == "")
                {
                    throw new Exception("logDirectory not found");
                }
                // if file name is empty
                else if (fileName == "")
                {
                    throw new Exception("fileName not found");
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("logDirectory") || logDirectory == "")
                {
                    logDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    ChangeLogDirectory(logDirectory);
                }
                else if (e.Message.Contains("fileName") || fileName == "")
                {
                    fileName = "tms.log";
                    //ChangeLogFileName(fileName);
                }
            }

            CustomTraceListener ctc = new CustomTraceListener($"{logDirectory}\\{fileName}");
            Trace.Listeners.Add(ctc);
            isSetup = true;
        }

        ///
        /// \brief Check if the logger is set up, if not, set it up. Call the writing function
        /// 
        /// \param message - <b>string</b> - message for the log 
        /// \param level - <b>LogLevel</b> - log level
        /// 
        public static void Log(string message, LogLevel level)
        {
            if (!isSetup) Setup();
            Trace.WriteLine(message, level.ToString());
        }


        ///
        /// \brief Change the directory of the log file
        /// Code inspired by https://www.c-sharpcorner.com/blogs/how-to-change-appconfig-data
        /// 
        /// \param newDirectory - <b>string</b> - new directory for the log file.
        /// \param level - <b>LogLevel</b> - log level
        /// 
        /// \return int
        public static int ChangeLogDirectory(string newDirectory)
        {
            string oldDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");

            // If the directory doesn't change, don't don anything
            if (oldDirectory == newDirectory) return 0;

            // Update the config file
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["LogDirectory"].Value = newDirectory;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
            
            // Move the log file from the old directory to the new one
            try
            {   
                // If the log was already set up previously
                if (isSetup)
                {
                    Logger.Log($"Log directory changed from \"{oldDirectory}\" to \"{newDirectory}\"", LogLevel.Information);

                    // Remove file from listener
                    if (Trace.Listeners.Count == 3)
                    {
                        Trace.Listeners.Remove(Trace.Listeners[2]);
                    }
                    
                    isSetup = false;
                }

                
                UpdateLogFileInNewDirectory(oldDirectory, newDirectory);

                // If logger not set up, do it
                if (!isSetup) Setup();

                return 0;
            }
            catch(Exception e)
            {
                Logger.Log($"We couldn't change the log directory. {e.Message}", LogLevel.Error);

                // Revert changes to the old directory
                configuration.AppSettings.Settings["LogDirectory"].Value = oldDirectory;
                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
                return 1;
            }
        }

        ///
        /// \brief Move the log file to the new directory
        /// 
        /// \param oldDirectory - <b>string</b> - old directory for the log file.
        /// \param newDirectory - <b>string</b> - new directory for the log file.
        /// 
        public static void UpdateLogFileInNewDirectory(string oldDirectory, string newDirectory)
        {
            string logFileName = ConfigurationManager.AppSettings.Get("LogFileName");
            
            string oldPath = String.Format($"{oldDirectory}\\{logFileName}");
            string newPath = String.Format($"{newDirectory}\\{logFileName}");

            try
            {
                // If the new directory doesn't exist, create
                if (!Directory.Exists(newDirectory))
                {
                    Directory.CreateDirectory(newDirectory);

                }

                // Copy the old file to new directory and overwrite if needed
                if (File.Exists(oldPath))
                {
                    File.Copy(oldPath, newPath, true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (isSetup)
            {
                Logger.Log($"Log file {logFileName} was moved from \"{oldDirectory}\" to \"{newDirectory}\"", LogLevel.Information);
            }
        }

        ///
        /// \brief Get the current directory of the log
        ///
        /// \return The current log directory as a string
        ///
        public static string GetCurrentLogDirectory()
        {
            string dict = ConfigurationManager.AppSettings.Get("LogDirectory");
            string logFileName = ConfigurationManager.AppSettings.Get("LogFileName");

            if (dict == "")
            {
                dict = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            }
            if (logFileName == "")
            {
                logFileName = "tms.log";
            }

            return String.Format($"{dict}\\{logFileName}"); ;
        }

        ///
        /// \brief This nested class is used to write the logs
        /// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.traceoptions?redirectedfrom=MSDN&view=net-6.0
        ///
        /// \return The current log directory as a string
        ///
        public class CustomTraceListener : TextWriterTraceListener
        {
            public CustomTraceListener(string file) : base(file) { }

            ///
            /// \brief Used to write line to file
            /// 
            /// \param message - <b>string</b> - string to write
            ///
            public override void WriteLine(string message)
            {
                string logDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");
                // If the new directory doesn't exist, create
                if (!Directory.Exists(logDirectory))
                {
                    // Try to create the directory, if it doesn't work, ignore
                    try { Directory.CreateDirectory(logDirectory); }
                    catch { }

                }
                // Get the Calling method
                var methodInfo = (new StackTrace()).GetFrame(5).GetMethod();

                // Get the Class for that method
                var classInfo = methodInfo.DeclaringType;

                base.Write((classInfo.Name + "[" + methodInfo.Name + "] ").PadRight(37) + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "    ---    ");
                base.WriteLine(message);

                Trace.Flush();
            }
        }

    }
}
