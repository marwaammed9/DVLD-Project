using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BusnissDVLD.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace BusnissDVLD
{
    public class clsApplication
    {

        public int ID { get; set; }
        public int AppPersonID { get; set; }
        public int CreatedByUserID { get; set; }

        public clsUser CreatedByUser { get; set; }
        public decimal PaidFees { get; set; }
        public DateTime LastStatusUpdate { get; set; }

        public int AppTypeID { get; set; }

        public clsAppType AppType { get; set; }
        public DateTime AppDate { get; set; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public enApplicationStatus AppStatus { get; set; } = enApplicationStatus.New;


        public string StatusText
        {
            get
            {

                switch (AppStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }

        public string ApplicantFullName
        {
            get
            {
                return clsPeople.FindPerson(AppPersonID).FullName;
            }
        }


        public bool Cancel()

        {
            return ApplicationAccess.UpdateStatus(ID, 2);
        }

        public bool SetComplete()

        {
            return ApplicationAccess.UpdateStatus(ID, 3);
        }


        public clsApplication(int ID, int appPersonID, int createdByUserID, decimal paidFees, DateTime lastStatusUpdate, enApplicationStatus appStatus, int appTypeID, DateTime appDat)
        {
            this.ID = ID;
            AppPersonID = appPersonID;
            CreatedByUserID = createdByUserID;
            PaidFees = paidFees;
            LastStatusUpdate = lastStatusUpdate;
            AppStatus = appStatus;
            AppTypeID = appTypeID;
            AppDate = appDat;
            AppType = clsAppType.FindAppType(appTypeID);
            CreatedByUser = clsUser.FindUser(createdByUserID);
            Mode = enMode.Update;
        }

        public clsApplication()
        {
            ID = -1;
            AppStatus = enApplicationStatus.New;
            PaidFees = -1;
            LastStatusUpdate = DateTime.Now;
            AppTypeID = -1;
            AppDate = DateTime.Now;
            CreatedByUserID = -1;
            CreatedByUser = new clsUser();
            AppType = new clsAppType();
            Mode = enMode.AddNew;
        }

        public static clsApplication FindApplicationByID(int ID)
        {
            int AppPersonID = -1, CreatedByUserID = -1, AppTypeID = -1;
            byte AppStatus = 0;
            decimal PaidFees = -1;
            DateTime LastStatusUpdate = DateTime.Now;
            DateTime AppDat = DateTime.Now;




            if (ApplicationAccess.GetApplicationByID(ID, ref AppPersonID, ref CreatedByUserID, ref PaidFees, ref LastStatusUpdate, ref AppStatus, ref AppTypeID, ref AppDat))
            {
                return new clsApplication(ID, AppPersonID, CreatedByUserID, PaidFees, LastStatusUpdate, (enApplicationStatus)AppStatus, AppTypeID, AppDat);

            }
            else
            {
                return null;

            }
        }

        private bool _AddNew()
        {
            this.ID = ApplicationAccess.AddApplication(AppPersonID, CreatedByUserID, PaidFees, LastStatusUpdate, (byte)AppStatus, AppTypeID, AppDate);

            return (this.ID != -1);
        }
        private bool _Update()
        {
            return (ApplicationAccess.UpdateApplication(ID, AppPersonID, CreatedByUserID, PaidFees, LastStatusUpdate, (byte)AppStatus, AppTypeID, AppDate));

        }

        public bool Delete()
        {
            return ApplicationAccess.DeleteApplication(this.ID);
        }

        public static DataTable GetApplications()
        {
            return ApplicationAccess.GetApplications();
        }

        public static bool IsExist(int ID)
        {
            return ApplicationAccess.IsExsit(ID);
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

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            return ApplicationAccess.GetActiveApplicationID(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return ApplicationAccess.GetActiveApplicationIDForLicenseClass(PersonID, ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(int ApplicationTypeID)
        {
            return GetActiveApplicationID(this.AppPersonID, ApplicationTypeID);
        }

    }
}

