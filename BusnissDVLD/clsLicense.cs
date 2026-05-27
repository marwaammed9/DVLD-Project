using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BusnissDVLD
{
    public class clsLicense
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo { get; set; }

        public int ID { get; set; }
        public int AppID { get; set; }
        public int DriverID { get; set; }
        public int CreatedByUserID { get; set; }
        public decimal PaidFees { get; set; }
        public DateTime IssuseDate { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClasses LicenseClassIfo { get; set; }

        public DateTime ExpDate { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssuseReason { get; set; }

        public clsDetainLicnese DetainInfo {  get; set; }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssuseReason);
            }
        }



        public clsLicense(int ID, int ApplicationID, int DriverID, int CreatedByUserID, decimal PaidFees, DateTime IssuseDate, DateTime ExpDate, int LicenseClassID,
            enIssueReason IssuseReason, string Notes, bool IsActive)
        {
            this.ID = ID;
            this.IsActive = IsActive;
            this.AppID = ApplicationID;
            this.Notes = Notes;
            this.LicenseClassID = LicenseClassID;
            this.PaidFees = PaidFees;
            this.DriverID = DriverID;
            this.IssuseDate = IssuseDate;
            this.ExpDate = ExpDate;
            this.CreatedByUserID = CreatedByUserID;
            this.IssuseReason = IssuseReason;
            LicenseClassIfo = clsLicenseClasses.GetClassByID(this.LicenseClassID);
            DriverInfo = clsDriver.FindDriverByID(DriverID);
            Mode = enMode.Update;
            DetainInfo = clsDetainLicnese.FindDetainLicensebyLicenseID(ID);



        }

        public clsLicense()
        {
            this.ID = -1;
            this.IsActive = true;
            this.AppID = -1;
            this.Notes = "";
            this.LicenseClassID = -1;
            this.PaidFees = -1;
            this.DriverID = -1;
            this.IssuseDate = DateTime.Now;
            this.ExpDate = DateTime.Now;
            this.CreatedByUserID = -1;
            this.IssuseReason = enIssueReason.FirstTime;
            Mode = enMode.AddNew;


        }


        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Replacement for Damaged";
                case enIssueReason.LostReplacement:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }


        public static clsLicense FindLicenseByID(int ID)
        {


            int AppID = -1;
            int DriverID = -1;
            int CreatedByUserID = -1;
            decimal PaidFees = -1;
            DateTime IssuseDate = DateTime.Now;
            int LicenseClassID = -1;
            DateTime ExpDate = DateTime.Now;
            string Notes = "";
            bool IsActive = false;
            byte IssuseReason = 0;



            if (LicenseAccess.GetLicenseByID(ID, ref AppID, ref DriverID, ref CreatedByUserID, ref PaidFees, ref IssuseDate, ref LicenseClassID, ref ExpDate, ref Notes, ref IsActive, ref IssuseReason))
            {
                return new clsLicense(ID, AppID, DriverID, CreatedByUserID, PaidFees, IssuseDate, ExpDate, LicenseClassID, (enIssueReason)IssuseReason, Notes, IsActive);

            }
            else
            {
                return null;

            }
        }
        public static clsLicense FindLicensebyAppID(int AppID)
        {


            int ID = -1;
            int DriverID = -1;
            int CreatedByUserID = -1;
            decimal PaidFees = -1;
            DateTime IssuseDate = DateTime.Now;
            int LicenseClassID = -1;
            DateTime ExpDate = DateTime.Now;
            string Notes = "";
            bool IsActive = false;
            byte IssuseReason = 0;



            if (LicenseAccess.GetLicenseByAppID(AppID, ref ID, ref DriverID, ref CreatedByUserID, ref PaidFees, ref IssuseDate, ref LicenseClassID, ref ExpDate, ref Notes, ref IsActive, ref IssuseReason))
            {
                return new clsLicense(ID, AppID, DriverID, CreatedByUserID, PaidFees, IssuseDate, ExpDate, LicenseClassID, (enIssueReason)IssuseReason, Notes, IsActive);

            }
            else
            {
                return null;

            }
        }
        public static int getActiveLicensebyClassIDAndPersonID(int ClassID, int Preson)
        {
            return LicenseAccess.IsActiveLicenseExistByPersonIDAndClassID(ClassID, Preson);

        }
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.AppPersonID = this.DriverInfo.PersonID;
            Application.AppDate = DateTime.Now;
            Application.AppTypeID = 5;
            Application.AppStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusUpdate = DateTime.Now;
            Application.PaidFees = clsAppType.FindAppType(5).Fees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = Application.ID;


            return this.DetainInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ID);

        }

        private bool _AddNew()
        {
            this.ID = LicenseAccess.AddLicnese(AppID, DriverID, CreatedByUserID, PaidFees, IssuseDate, LicenseClassID, ExpDate, Notes, IsActive, (byte)IssuseReason);

            return (this.ID != -1);
        }
        private bool _Update()
        {
            return (LicenseAccess.UpdateLicense(ID, AppID, DriverID, CreatedByUserID, PaidFees, IssuseDate, LicenseClassID, ExpDate, Notes, IsActive, (byte)IssuseReason));

        }


        public clsLicense RenewLicense(int CreatedByUserID)
        {
            clsApplication app = new clsApplication();
            app.AppStatus = clsApplication.enApplicationStatus.Completed;
            app.CreatedByUserID = CreatedByUserID;
            app.PaidFees = clsAppType.FindAppType(2).Fees;
            app.AppDate = DateTime.Now;
            app.AppPersonID = this.DriverInfo.PersonID;
            app.AppTypeID = 2;
            app.LastStatusUpdate = DateTime.Now;
            if (!app.Save())
            {
                return null;
            }
            clsLicense newLicense = new clsLicense();
            newLicense.AppID = app.ID;
            newLicense.CreatedByUserID = app.CreatedByUserID;
            newLicense.DriverID = this.DriverID;
            newLicense.Notes = this.Notes;
            newLicense.IsActive = true;
            newLicense.IssuseDate = DateTime.Now;
            newLicense.LicenseClassID = this.LicenseClassID;
            newLicense.IssuseReason = enIssueReason.Renew;
            newLicense.PaidFees = this.LicenseClassIfo.ClassFees;
            newLicense.ExpDate = DateTime.Now.AddYears(this.LicenseClassIfo.DefaultValidityLenght);

            if (!newLicense.Save())
            {
                return null;
            }
            DeactivateCurrentLicense();

            return newLicense;

        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            Application.AppPersonID = this.DriverInfo.PersonID;
            Application.AppDate = DateTime.Now;

            Application.AppTypeID = (IssueReason == enIssueReason.DamagedReplacement) ? 4 : 3;

            Application.AppStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusUpdate = DateTime.Now;
            Application.PaidFees = clsAppType.FindAppType(Application.AppTypeID).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.AppID = Application.ID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssuseDate = DateTime.Now;
            NewLicense.ExpDate = this.ExpDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = true;
            NewLicense.IssuseReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }



        public static DataTable GetLicenses()
        {
            return LicenseAccess.GetLicenses();
        }
        public static DataTable GetLicensesByDriverID(int DriverID)
        {
            return LicenseAccess.GetLicensesByDriverID(DriverID);
        }
        public Boolean IsLicenseExpired()
        {

            return (this.ExpDate < DateTime.Now);

        }

        public int Detain(decimal fees, int CreatedByUserID)
        {
            clsDetainLicnese detainedLicense = new clsDetainLicnese();
            detainedLicense.LicenseID = this.ID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = fees;
            detainedLicense.CreatedByUserID = CreatedByUserID;


            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.ID;


        }


        public bool DeactivateCurrentLicense()
        {


            return (LicenseAccess.DeactivateLicense(this.ID));
        }
        public static bool IsExist(int ID)
        {
            return LicenseAccess.IsExsit(ID);
        }
        public static bool IsDetain(int ID)
        {
            return LicenseAccess.IsDetain(ID);
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
