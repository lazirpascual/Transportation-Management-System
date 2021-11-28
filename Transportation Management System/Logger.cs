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
    class Logger
    {
        string logDirectory; /// What is the directory where the log will be located?

        private static bool isSetup = false;
        public static void Setup()
        {
            string fileName;
            try
            {
                fileName = ConfigurationManager.AppSettings.Get("LogFileName");
            }
            catch
            {
                // If the user doesn't set the LogFileName, set the log name to server.log by default
                fileName = "server.log";
            }
            string currentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            CustomTraceListener ctc = new CustomTraceListener($"{currentPath}\\{fileName}");
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

                base.Write((classInfo.Name + "[" + methodInfo.Name + "] ").PadRight(27) + DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss tt") + "    ---    ");
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
