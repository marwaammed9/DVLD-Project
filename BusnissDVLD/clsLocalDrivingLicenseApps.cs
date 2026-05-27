using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using static BusnissDVLD.clsApplication;
using static System.Net.Mime.MediaTypeNames;


namespace BusnissDVLD
{
    public class clsLocalDrivingLicenseApps : clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalID { get; set; }
        public int ClassID { get; set; }

        public clsLicenseClasses LicenseClassInfo { get; set; }
        public string PersonFullName
        {
            get
            {
                return base.ApplicantFullName;
            }

        }


        public clsLocalDrivingLicenseApps()
        {
            LocalID = -1;
            ClassID = -1;
            Mode = enMode.AddNew;


        }

        public clsLocalDrivingLicenseApps(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             decimal PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this.LocalID = LocalDrivingLicenseApplicationID;
            this.ID = ApplicationID;
            this.AppPersonID = ApplicantPersonID;
            this.AppDate = ApplicationDate;
            this.AppTypeID = ApplicationTypeID;
            this.AppStatus = ApplicationStatus;
            this.LastStatusUpdate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.ClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClasses.GetClassByID(ClassID);
            Mode = enMode.Update;

        }

        public static DataTable GetAllLocalApps()
        {
            return LocalDrivingLicenseAppsAccess.GetAllLocalLicenseApplications();
        }

        public static clsLocalDrivingLicenseApps FindLocalAppByLocalID(int LocalID)
        {
            int AppID = -1;
            int ClassID = -1;

            bool IsFound = LocalDrivingLicenseAppsAccess.GetLocalDrivingLicenseApplicationInfoByID
             (LocalID, ref AppID, ref ClassID);


            if (IsFound)
            {
                clsApplication app = clsApplication.FindApplicationByID(AppID);
                return new clsLocalDrivingLicenseApps(LocalID, AppID, app.AppPersonID, app.AppDate, app.AppTypeID, app.AppStatus, app.LastStatusUpdate
                    , app.PaidFees, app.CreatedByUserID, ClassID);

            }
            else
            {
                return null;

            }
        }

        public static clsLocalDrivingLicenseApps FindLocalAppByAppID(int AppID)
        {
            int LocalID = -1;
            int ClassID = -1;


            bool IsFound = LocalDrivingLicenseAppsAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (AppID, ref LocalID, ref ClassID);


            if (IsFound)
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindApplicationByID(AppID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseApps(
                    LocalID, AppID,
                    Application.AppPersonID,
                                     Application.AppDate, Application.AppTypeID,
                                    (enApplicationStatus)Application.AppStatus, Application.LastStatusUpdate,
                                     Application.PaidFees, Application.CreatedByUserID, ClassID);
            }
            else
                return null;

        }


        private bool _AddNewLocal()
        {

            this.LocalID = LocalDrivingLicenseAppsAccess.AddNewLocalDrivingLicenseApplication
                (
                this.ID, this.ClassID);

            return (this.LocalID != -1);
        }
        private bool _UpdateLocalApp()
        {

            return (LocalDrivingLicenseAppsAccess.UpdateLocalDrivingLicenseApplication
                  (this.LocalID, this.ID, this.ClassID));

        }

        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;


            //After we save the main application now we save the sub application.
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocal())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalApp();

            }

            return false;
        }
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = LocalDrivingLicenseAppsAccess.DeleteLocalDrivingLicenseApplication(this.LocalID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;

        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.DoesPassTestType(this.LocalID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.DoesAttendTestType(this.LocalID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return LocalDrivingLicenseAppsAccess.TotalTrialsPerTest(this.LocalID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestType.enTestType TestTypeID)

        {
            return LocalDrivingLicenseAppsAccess.TotalTrialsPerTest(this.LocalID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {

            return LocalDrivingLicenseAppsAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return LocalDrivingLicenseAppsAccess.IsThereAnActiveScheduledTest(this.LocalID, (int)TestTypeID);
        }

        public static clsTest GetLastTestPerTestType(int LocalID, clsTestType.enTestType TestTypeID)
        {
            int TestID = clsTest.GetLastTestByAndTestTypeAndLocalApp(LocalID, TestTypeID);
            if (TestID == -1)
            {
                return null;
            }

            return clsTest.FindByID(TestID);
        }


        public bool PassedAllTests()
        {
            if (clsTest.GetPassedTestCount(this.LocalID) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            if (clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindDriverByPrseonID(this.AppPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.AppPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.AppID = this.ID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.ClassID;
            License.IssuseDate = DateTime.Now;
            License.ExpDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLenght);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssuseReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.ID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.getActiveLicensebyClassIDAndPersonID(this.AppPersonID, this.ClassID);
        }
    }
}





