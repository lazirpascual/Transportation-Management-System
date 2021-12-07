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

        private DAL db = new DAL();

        ///
        /// \brief This constructor is used to initialize the log file name and directory
        /// 
        /// 
        ///        
        public Admin()
        {
            LogFile = "tms.log";
            LogDirectory = Directory.GetCurrentDirectory();
        }

        ///
        /// \brief This method is called to view the current log files
        /// 
        /// 
        /// \return Returns list of current log files
        ///  
        public string ViewLogFiles()
        {
                              
            return Logger.GetCurrentLogDirectory();
                        
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
        public int FetchCarrierID(string name)
        {
            int ID = db.GetCarrierIdByName(name);
            return ID;
        }


        ///
        /// \brief This method is called in order to update the rate/fee for the TMS application
        /// 
        /// \param rate  - <b>string</b> - value of the rate
        /// 
        /// \return Returns result of the query after we perform the update rate command
        /// 
        public void UpdateCity(CarrierCity cCity, City oldCity)
        {
            db.UpdateCarrierCity(cCity, oldCity);


        }

        public List<CarrierCity> GetCitiesByCarrier(string carrierName)
        {
            List<CarrierCity> carrierCities = db.FilterCitiesByCarrier(carrierName);
            return carrierCities;
        }

        public void CarrierDeletion(Carrier carrier)
        {
            db.DeleteCarrier(carrier);
        }

        public long CarrierCreation(Carrier carrier)
        {
            return db.CreateCarrier(carrier);
        }

        public void CarrierCity(CarrierCity carrierCity, int CR)
        {
            if(CR==0)
            {
                db.RemoveCarrierCity(carrierCity);
            }
            else
            {
                db.CreateCarrierCity(carrierCity);
            }
            
        }


        public List<Carrier> FetchCarriers()
        {
            List<Carrier> carriers = db.GetAllCarriers();
            return carriers;
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
        public void UpdateCarrierInfo(Carrier newCarrier)
        {

            db.UpdateCarrier(newCarrier);
        }

        ///
        /// \brief This method is called in order to update the the current route
        /// 
        /// \param newInfo  - <b>string</b> - updated route
        /// 
        /// \return Returns string
        /// 
        public void UpdateRouteAD(Route newRoute)
        {

            db.UpdateRoute(newRoute);
        }


        public List<Route> GetRoutesAD()
        {
            return db.GetRoutes();
        }


        public void UpdateDatabaseConString(string fieldToChange, string newData)
        {
            db.UpdateDatabaseConnectionString(fieldToChange, newData);
        }
        ///
        /// \brief This method is called to create a backup for the TMS application
        /// 
        /// 
        /// \return Returns TRUE if backup is successful, else FALSE
        /// 
        public void Backup(string backUpFilePath)
        {
            db.BackupDatabase(backUpFilePath);

        }

        ///
        /// \brief This method is called to create a user for the TMS application
        /// 
        /// 
        /// \return Returns TRUE if backup is successful, else FALSE
        /// 
        public bool CreateAUser(User user)
        {
            DAL db = new DAL();
            bool userCreated = false;
            if(db.CreateUser(user)==true)
            {
                userCreated = true;
            }
            return userCreated;
        }
    }
}
