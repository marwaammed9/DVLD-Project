using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsDriver
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsPeople PersonInfo;

        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public int CreatedByUserID { get; set; }

        public DateTime CreatedDate { get; set; }


        public clsDriver()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        public clsDriver(int DriverID, int PersonID, int CreatedUserID, DateTime CreatedTime)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedUserID;
            this.CreatedDate = CreatedTime;
            this.PersonInfo = clsPeople.FindPerson(PersonID);
            Mode = enMode.Update;
        }

        public static DataTable GetAllDrivers()
        {
            return DriverAccess.GetDrivers();
        }

        public static DataTable GetAllMyDrivers()
        {
            return DriverAccess.GetMyDrivers();
        }


        public static clsDriver FindDriverByID(int ID)
        {
            int PersonID = -1, CreatedByUserID = -1;

            DateTime CreatedTime = DateTime.Now;



            if (DriverAccess.GetDriverByID(ID, ref PersonID, ref CreatedByUserID, ref CreatedTime))
            {
                return new clsDriver(ID, PersonID, CreatedByUserID, CreatedTime);

            }
            else
            {
                return null;

            }
        }

        public static clsDriver FindDriverByPrseonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;

            DateTime CreatedTime = DateTime.Now;



            if (DriverAccess.GetDriverPersonIDByID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedTime))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedTime);

            }
            else
            {
                return null;

            }
        }
        public static bool HasInternatioanlLicense(int DriverID)
        {
            if (DriverAccess.GetActiveInternationalLicenseIDByDriverID(DriverID) == -1)
            {
                return false;
            }
            else
            {
                return true;

            }
        }
        public static clsInternationalLicense GetInternationlLicense(int DriverID)
        {
            if (DriverAccess.GetActiveInternationalLicenseIDByDriverID(DriverID) != -1)
            {
                return clsInternationalLicense.FindInternatioanlByID(clsInternationalLicense.GetInternationalLicenseByDriverID(DriverID));
            }
            else
            {
                return null;

            }
        }

        private bool _AddNew()
        {
            this.DriverID = DriverAccess.AddDriver(PersonID, CreatedByUserID, CreatedDate);

            return (this.DriverID != -1);
        }

        private bool _Update()
        {
            return (DriverAccess.UpdateDriver(DriverID, PersonID, CreatedByUserID, CreatedDate));

        }


        public static bool IsExist(int ID)
        {
            return DriverAccess.IsDriverExist(ID);
        }


        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    if (_Update())
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;

        }

        public DataTable GetLicenses()
        {
            return clsLicense.GetLicensesByDriverID(this.DriverID);
        }
        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetLicensesByDriverID(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return InternationalLicense.GetInternationalLicensesByDriverID(DriverID);
        }
        public DataTable GetInternationalLicenses()
        {
            return InternationalLicense.GetInternationalLicensesByDriverID(this.DriverID);
        }

    }
}
