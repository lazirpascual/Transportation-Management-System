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
    public static class Logger
    {
        // Current directory is the default one

        private static bool isSetup = false;

        /*  -- Method Header Comment
	    Name	:	Setup
	    Purpose :	Set up the LogClass if it hasn't been setup already
	    Inputs	:	string message, LogLevel level
	    Outputs	:	Nothing
	    Returns	:	Nothing
        */
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

        /*  -- Method Header Comment
	    Name	:	Log
	    Purpose :	Check if the logger is set up, if not, set it up.
                    Call the writing function
	    Inputs	:	string message, LogLevel level
	    Outputs	:	Nothing
	    Returns	:	Nothing
        */
        public static void Log(string message, LogLevel level)
        {
            if (!isSetup) Setup();
            Trace.WriteLine(message, level.ToString());
        }


        /*  -- Method Header Comment
	    Name	:	ChangeLogDirectory
	    Purpose :	Change the directory of the log file
            Code inspired by https://www.c-sharpcorner.com/blogs/how-to-change-appconfig-data
	    Inputs	:	string message
	    Outputs	:	Nothing
	    Returns	:	Nothing
        */
        public static void ChangeLogDirectory(string newDirectory)
        {
            string oldDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");

            // If the directory doesn't change, don't don anything
            if (oldDirectory == newDirectory) return;

            // Update the config file
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["LogDirectory"].Value = newDirectory;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
            
            // Move the log file from the old directory to the new one
            try
            {
                UpdateLogFileInNewDirectory(oldDirectory, newDirectory);

                // If the log was already set up previously
                if (isSetup && Trace.Listeners.Count == 3)
                {
                    Logger.Log($"Log directory changed from \"{oldDirectory}\" to \"{newDirectory}\"", LogLevel.Information);
                    Trace.Listeners.Remove(Trace.Listeners[2]);
                    isSetup = false;
                }
            }
            catch(Exception e)
            {
                Logger.Log($"We couldn't change the log directory. {e.Message}", LogLevel.Error);

                // Revert changes to the old directory
                configuration.AppSettings.Settings["LogDirectory"].Value = oldDirectory;
                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }


        /*  -- Method Header Comment
	    Name	:	UpdateLogFileInNewDirectory
	    Purpose :	Move the log file to the new directory
	    Inputs	:	string oldDirectory
                    string newDirectory
	    Outputs	:	Nothing
	    Returns	:	Nothing
        */
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

                // Move file to the new log folder
                // Ignore if there are no files to be moved
                if (File.Exists(oldPath))
                {
                    File.Move(oldPath, newPath);
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


        /*  -- Method Header Comment
	    Name	:	GetCurrentLogDirectory
	    Purpose :	Get the current directory of the log
	    Inputs	:	Nothing
	    Outputs	:	Nothing
	    Returns	:	LogDirectory
        */
        public static string GetCurrentLogDirectory()
        {
            return ConfigurationManager.AppSettings.Get("LogDirectory");
        }


        // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.traceoptions?redirectedfrom=MSDN&view=net-6.0
            /*  -- Nested Class Header Comment
            Name	:	CustomTraceListener
            Purpose :  To write the logs
            */
        public class CustomTraceListener : TextWriterTraceListener
        {
            public CustomTraceListener(string file) : base(file) { }

            /*  -- Method Header Comment
	        Name	:	WriteLine
	        Purpose :	Write line to file
	        Inputs	:	string message
	        Outputs	:	Nothing
	        Returns	:	Nothing
            */
            public override void WriteLine(string message)
            {
                // Get the Calling method
                var methodInfo = (new StackTrace()).GetFrame(5).GetMethod();

                // Get the Class for that method
                var classInfo = methodInfo.DeclaringType;

                base.Write((classInfo.Name + "[" + methodInfo.Name + "] ").PadRight(37) + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "    ---    ");
                base.WriteLine(message);

                Trace.Flush();
            }
        }

        ///
        /// \brief Write some content to a log file
        /// 
        /// \param level  - <b>LogLevel</b> -
        /// \param origin - <b>LogOrigin</b> - 
        /// \param fileName- <b>string</b> - 
        /// \param currentTime- <b>TimeStamp</b> - 
        /// 
        /// \return void
        ///
        //public void WriteLog(logLevel level, logOrigin origin, string fileName, Timestamp currentTime)
        //{

        //}

        ///
        /// \brief Change the directory from the log file
        /// 
        /// \param directory  - <b>string</b> - the complete path of the new directory location 
        /// 
        /// \return bool
        ///
        //public bool ChangeLogDirectory(string directory)
        //{

        //} 


    }
}
