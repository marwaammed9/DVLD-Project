using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsDetainLicnese
    {
        public int ID { get; set; }
        public int LicenseID { get; set; }
        public int CreatedByUserID { get; set; }
        public decimal FineFees { get; set; }
        public DateTime DetainDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleaseApplicationID { get; set; }
        public bool IsReleased { get; set; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;



        public clsDetainLicnese(int iD, int licenseID, int createdByUserID, decimal fineFees, DateTime detainDate, int? releasedByUserID, DateTime? releaseDate, int? releaseApplicationID, bool isReleased)
        {
            ID = iD;
            LicenseID = licenseID;
            CreatedByUserID = createdByUserID;
            FineFees = fineFees;
            DetainDate = detainDate;
            ReleasedByUserID = releasedByUserID;
            ReleaseDate = releaseDate;
            ReleaseApplicationID = releaseApplicationID;
            IsReleased = isReleased;
            Mode = enMode.Update;
        }

        public clsDetainLicnese()
        {
            this.ID = 0;
            this.LicenseID = 0;
            this.CreatedByUserID = 0;
            this.FineFees = 0;
            this.DetainDate = DateTime.Now;
            this.ReleasedByUserID = null;
            this.ReleaseDate = null;
            this.ReleaseApplicationID = null;
            this.IsReleased = false;
            Mode = enMode.AddNew;
        }

        public static clsDetainLicnese FindDetainLicenseByID(int ID)
        {


            int licenseID = -1;
            int createdByUserID = -1;
            decimal fineFees = -1;
            DateTime detainDate = DateTime.Now;
            int? releasedByUserID = null;
            DateTime? releaseDate = null;
            int? releaseApplicationID = null;
            bool isReleased = false;



            if (DetainLicenseAccess.GetDetainLicenseByID(ID, ref licenseID, ref createdByUserID, ref fineFees, ref detainDate, ref releasedByUserID, ref releaseDate, ref releaseApplicationID, ref isReleased))
            {
                return new clsDetainLicnese(ID, licenseID, createdByUserID, fineFees, detainDate, releasedByUserID, releaseDate, releaseApplicationID, isReleased);

            }
            else
            {
                return null;

            }
        }
        public static clsDetainLicnese FindDetainLicensebyLicenseID(int LicenseID)
        {


            int ID = -1;
            int createdByUserID = -1;
            decimal fineFees = -1;
            DateTime detainDate = DateTime.Now;
            int? releasedByUserID = null;
            DateTime? releaseDate = null;
            int? releaseApplicationID = null;
            bool isReleased = false;



            if (DetainLicenseAccess.GetDetainLicenseByLicenseID(LicenseID, ref ID, ref createdByUserID, ref fineFees, ref detainDate, ref releasedByUserID, ref releaseDate, ref releaseApplicationID, ref isReleased))
            {
                return new clsDetainLicnese(ID, LicenseID, createdByUserID, fineFees, detainDate, releasedByUserID, releaseDate, releaseApplicationID, isReleased);

            }
            else
            {
                return null;


            }
        }

        private bool _AddNew()
        {
            this.ID = DetainLicenseAccess.AddDetainLicnese(LicenseID, CreatedByUserID, FineFees, DetainDate, ReleasedByUserID, ReleaseDate, ReleaseApplicationID, IsReleased);

            return (this.ID != -1);
        }
        private bool _Update()
        {
            return (DetainLicenseAccess.UpdateDetainLicense(ID, LicenseID, CreatedByUserID, FineFees, DetainDate, ReleasedByUserID, ReleaseDate, ReleaseApplicationID, IsReleased));

        }

        //public static bool DeleteLisence(int ID)
        //{
        //    return LicenseAccess.de(ID);
        //}

        public static DataTable GetDetainLicenses()
        {
            return DetainLicenseAccess.GetDetainLicenses();
        }
        public static DataTable GetDetainLicensesForMain()
        {
            return DetainLicenseAccess.GetDetainLicensesForMain();
        }


        public static bool IsExsit(int ID)
        {
            return DetainLicenseAccess.IsDetain(ID);
        }

        public static bool IsDetain(int ID)
        {
            return DetainLicenseAccess.IsDetainByLicenseID(ID);
        }

        public bool ReleaseDetainedLicense(int ReleaseUserID, int ReleaseAppLicationID)
        {
            return DetainLicenseAccess.ReleaseDetainLicense(this.ID, ReleaseUserID, DateTime.Now, ReleaseAppLicationID);
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

    }
}
