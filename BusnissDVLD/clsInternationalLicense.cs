using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BusnissDVLD.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace BusnissDVLD
{
    public class clsInternationalLicense :clsApplication
    {
        public int InterLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo {  get; set; }
        public int AppID { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpDate { get; set; }
        public int IssuedUsingLicenseID { get; set; }

        public clsLicense Issuedlicense { get; set; }
        public bool IsActive { get; set; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
             DateTime ApplicationDate,
              enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
              decimal PaidFees, int CreatedByUserID,
              int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
             DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
            //this is for the base clase
            base.ID = ApplicationID;
            base.AppPersonID = ApplicantPersonID;
            base.AppDate = ApplicationDate;
            base.AppTypeID = 6;
            base.AppStatus = ApplicationStatus;
            base.LastStatusUpdate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InterLicenseID = InternationalLicenseID;
            this.DriverID = DriverID;
            this.IssuedUsingLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindDriverByID(this.DriverID);

            Mode = enMode.Update;
        
        }

        public clsInternationalLicense()
        {
            ID = -1;
            DriverID = -1;
          
            CreatedByUserID = -1;
            IsActive = true;
            InterLicenseID = -1;
            IssuedUsingLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpDate = DateTime.Now;

            Mode = enMode.AddNew;

        }

        public static DataTable GetInternationalLicense()
        {
            return InternationalLicense.GetInternatioanlLicenses();
        }
        public static int GetInternationalLicenseByDriverID(int DriverID)
        {
            return DriverAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public static clsInternationalLicense FindInternatioanlByID(int ID)
        {

            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (InternationalLicense.GetInternationaLicenseByID(ID,ref ApplicationID,ref DriverID,ref CreatedByUserID,ref IssueDate,ref ExpirationDate,ref IssuedUsingLocalLicenseID,ref IsActive)){ 
            
                //now we find the base application
                clsApplication Application = clsApplication.FindApplicationByID(ApplicationID);


                return new clsInternationalLicense(Application.ID,
                    Application.AppPersonID,
                                     Application.AppDate,
                                    (enApplicationStatus)Application.AppStatus, Application.LastStatusUpdate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     ID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;
        }
        public static clsInternationalLicense FindInternatioanlByAppID(int AppID)
        {


            int DriverID = -1,
              ID = -1,
              CreatedByUserID = -1;
            bool IsActive = false;
            int IssuedUsingLocalLicenseID = 0;
            DateTime IssuseDate = DateTime.Now,
              ExpDate = DateTime.Now;

            if (InternationalLicense.GetInternationalLicenseByAppID(AppID, ref ID, ref DriverID, ref CreatedByUserID, ref IssuseDate, ref ExpDate, ref IssuedUsingLocalLicenseID, ref IsActive))
            {
                clsApplication Application = clsApplication.FindApplicationByID(AppID);


                return new clsInternationalLicense(Application.ID,
                    Application.AppPersonID,
                                     Application.AppDate,
                                    (enApplicationStatus)Application.AppStatus, Application.LastStatusUpdate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     ID, DriverID, IssuedUsingLocalLicenseID,
                                         IssuseDate, ExpDate, IsActive);

            }
            else
            {
                return null;
            }
        }




        public static bool IsExist(int ID)
        {
            return InternationalLicense.IsExsit(ID);
        }

        private bool _AddNew()
        {
            this.ID = InternationalLicense.AddInternationalLicnese(ID, DriverID, CreatedByUserID, IssueDate, ExpDate, IssuedUsingLicenseID, IsActive);

            return (this.ID != -1);
        }

        private bool _Update()
        {
          return InternationalLicense.UpdateInternationalLicense(InterLicenseID,AppID,DriverID,IssuedUsingLicenseID,IssueDate,ExpDate,IsActive,CreatedByUserID);

        }
        public bool Save()
        {

            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

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

                    return _Update();

            }

            return false;
        }



    }
}
