using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Transportation_Management_System
{
    /// 
    /// \class Admin
    /// 
    /// \brief The purpose of this class is to represent the Admin User
    ///
    /// This class represents the role of an Admin User, which represents a User who has IT experience
    /// and is tasked with the configuration, maintenance, and troubleshooting of the TMS application.
    ///
    /// \author <i>Team Blank</i>
    ///
    class Admin : User
    {
        private string LogDirectory { get; set; }
        private string LogFile { get; set; }

        ///
        /// \brief This method is called to view the current log files
        /// 
        /// 
        /// \return Returns list of current log files
        ///
        
        public Admin()
        {
            LogFile = "tms.log";
            LogDirectory = Directory.GetCurrentDirectory();
        }
        public string ViewLogFiles()
        {
            string logDict = Logger.GetCurrentLogDirectory();
            string logFileName = ConfigurationManager.AppSettings.Get("LogFileName");
            string newPath = logDict + logFileName;
                           
            return newPath;
                        
        }

        ///
        /// \brief This method is called in order to change the directory 
        /// of where the log files are currently being written.
        /// 
        /// \param newDirectory  - <b>string</b> - path to the new directory
        /// 
        /// \return Returns void
        /// 
        public bool ChangeLogDirectory(string newDirectory)
        {
            int result = Logger.ChangeLogDirectory(newDirectory);

            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       

        ///
        /// \brief This method is called in order to update the rate/fee for the TMS application
        /// 
        /// \param rate  - <b>string</b> - value of the rate
        /// 
        /// \return Returns result of the query after we perform the update rate command
        /// 
        public string UpdateRate(string rate)
        {

            return "";
        }

        ///
        /// \brief This method is called in order to update the carrier information
        /// 
        /// \param newInfo  - <b>string</b> - updated information
        /// 
        /// \return Returns result of the query after we perform the update carrier command
        /// 
        public string UpdateCarrierInfo(string newInfo)
        {

            return "";
        }

        ///
        /// \brief This method is called in order to update the the current route
        /// 
        /// \param newInfo  - <b>string</b> - updated route
        /// 
        /// \return Returns string
        /// 
        public string UpdateRoute(string newRoute)
        {

            return "";
        }

        ///
        /// \brief This method is called to create a backup for the TMS application
        /// 
        /// 
        /// \return Returns TRUE if backup is successful, else FALSE
        /// 
        public string CreateBackup()
        {

            return "";
        }
    }
}
