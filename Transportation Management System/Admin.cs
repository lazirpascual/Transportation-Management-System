/* -- FILEHEADER COMMENT --
    FILE		:	Admin.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Admin class which inherits from User class.

*/

using System.Collections.Generic;

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
    internal class Admin : User
    {
        // object to access the database
        private readonly DAL db = new DAL();

        ///
        /// \brief This method is called to view the current log files
        ///
        ///
        /// \return list of current log files
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
        /// \return true - if log directory was successfully changed, false - if unsuccessful
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
        /// \brief This method is called in order to get the carrier ID of a specific carrier
        ///
        /// \param name  - <b>string</b> - name of the carrier
        ///
        /// \return the ID of the carrier
        ///
        public int FetchCarrierID(string name)
        {
            int ID = db.GetCarrierIdByName(name);
            return ID;
        }

        ///
        /// \brief This method is called in order to update the depot city of a carrier
        ///
        /// \param cCity  - <b>CarrierCity</b> - CarrierCity object with all it's properties
        /// \param oldCity  - <b>City</b> - value of the previous depot city of the carrier
        ///
        /// \return None - void
        ///
        public void UpdateCity(CarrierCity cCity, City oldCity)
        {
            db.UpdateCarrierCity(cCity, oldCity);
        }

        ///
        /// \brief This method is called in order to get a list of depot cities of a carrier.
        ///
        /// \param carrierName  - <b>string</b> - name of carrier.
        ///
        /// \return carrierCities - <b>List<CarrierCity></b> - list of depot cities of the carrier.
        ///
        public List<CarrierCity> GetCitiesByCarrier(string carrierName)
        {
            List<CarrierCity> carrierCities = db.FilterCitiesByCarrier(carrierName);
            return carrierCities;
        }

        ///
        /// \brief This method is called in order to delete a specific carrier from the TMS database.
        ///
        /// \param carrier  - <b>Carrier</b> - Carrier object with all it's properties.
        ///
        /// \return None - void
        ///
        public void CarrierDeletion(Carrier carrier)
        {
            db.DeleteCarrier(carrier);
        }

        ///
        /// \brief This method is called in order to create a new carrier in the TMS database.
        ///
        /// \param carrier  - <b>Carrier</b> - Carrier object with all it's properties.
        ///
        /// \return ID - <b>long</b> - the ID of the new carrier
        ///
        public long CarrierCreation(Carrier carrier)
        {
            return db.CreateCarrier(carrier);
        }

        ///
        /// \brief This method is called in order to remove or create a carrier city.
        ///
        /// \param carrierCity  - <b>CarrierCity</b> - CarrierCity object with all it's properties.
        /// \param CR  - <b>int</b> - integer value to indicate creation or deletion.
        ///
        /// \return None - void
        ///
        public void CarrierCity(CarrierCity carrierCity, int CR)
        {
            if (CR == 0)
            {
                db.RemoveCarrierCity(carrierCity);
            }
            else
            {
                db.CreateCarrierCity(carrierCity);
            }
        }

        ///
        /// \brief This method is called in order to get a list of all carrier in the TMS.
        ///
        /// \param None
        ///
        /// \return carriers - <b>List<Carrier></b> - list of all carriers.
        ///
        public List<Carrier> FetchCarriers()
        {
            List<Carrier> carriers = db.GetAllCarriers();
            return carriers;
        }

        ///
        /// \brief This method is called in order to get the rates from the TMS database.
        ///
        /// \param None
        ///
        /// \return OSHTRates - <b>Rate</b> - rate from the database.
        ///
        public Rate FetchOSHTRates()
        {
            return db.GetOSHTRates();
        }

        ///
        /// \brief Update an existing carrier's attributes.
        ///
        /// \param newCarrier  - <b>Carrier</b> - The new carrier information to be used in the update
        ///
        /// \return None - void
        ///
        public void UpdateCarrierInfo(Carrier newCarrier)
        {
            db.UpdateCarrier(newCarrier);
        }

        ///
        /// \brief Update an existing route's attributes
        ///
        /// \param newRoute  - <b>Route</b> - The new route information to be used in the update.
        ///
        /// \return None - void
        ///
        public void UpdateRouteAD(Route newRoute)
        {
            db.UpdateRoute(newRoute);
        }

        ///
        /// \brief Used to get a list of the routes form the TMS database.
        ///
        /// \param None
        ///
        /// \return A list of routes.
        ///
        public List<Route> GetRoutesAD()
        {
            return db.GetRoutes();
        }

        ///
        /// \brief Used to modify the database connection string.
        ///
        /// \param fieldToChange  - <b>string</b> - name of field to change.
        /// \param newData  - <b>string</b> - new value of the field.
        ///
        /// \return None - void
        ///
        public void UpdateDatabaseConString(string fieldToChange, string newData)
        {
            db.UpdateDatabaseConnectionString(fieldToChange, newData);
        }

        ///
        /// \brief This method is called to create a backup for the TMS application.
        ///
        /// \param backUpFilePath - <b>string</b> - file path of the backup.
        ///
        /// \return None - void
        ///
        public void Backup(string backUpFilePath)
        {
            db.BackupDatabase(backUpFilePath);
        }

        ///
        /// \brief This method is called to create a user for the TMS application
        ///
        /// \param user - <b>User</b> - file path of the backup.
        ///
        /// \return true - if user is created successfully, false - if unsuccessful.
        ///
        public bool CreateAUser(User user)
        {
            bool userCreated = false;
            if (db.CreateUser(user) == true)
            {
                userCreated = true;
            }
            return userCreated;
        }

        ///
        /// \brief This method is called to update the rate in the TMS database.
        ///
        /// \param rate - <b>double</b> - new rate.
        /// \param type - <b>RateType</b> - rate type.
        ///
        /// \return None - void
        ///
        public void UpdateRate(double rate, RateType type)
        {
            db.UpdateOSHTRate(rate, type);
        }
    }
}