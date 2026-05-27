using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsTestAppintment
    {
        public int ID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public decimal PaidFees { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public int? ReTakeTestApplicationID {  get; set; }

        public  clsApplication RetakeTestAppInfo { set; get; }




        enum eModes { eNew = 1, eUpdate = 2 };
        eModes Mode;




        public clsTestAppintment()
        {
            CreatedByUserID = -1;
            IsLocked = false;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            LocalDrivingLicenseApplicationID = -1;
            TestTypeID = -1;
            ID = -1;
            ReTakeTestApplicationID = -1;
            Mode = eModes.eNew;

        }

        public clsTestAppintment(int ID, int TestTypeID, int LocalDrivingLicenseApplicationID, decimal PaidFees, DateTime AppointmentDate, int CreatedByUserID, int ReTakeTestApplicationID, bool IsLocked)
        {
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = false;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.TestTypeID = TestTypeID;
            this.IsLocked = IsLocked;
            this.ReTakeTestApplicationID = ReTakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindApplicationByID(ReTakeTestApplicationID);
            this.ID = ID;
            Mode = eModes.eUpdate;
        }


        public static DataTable GetAllTestAppointments()
        {
            return AppointmentTestAccess.GetAppointments();
        }
        public static DataTable GetAllTestAppointmentsWith_LocalIDAndTestType(int LocalID, int TestID)
        {
            return AppointmentTestAccess.GetAllAppointmentByLocalIDAndTestID(LocalID, TestID);
        }

        public static clsTestAppintment FindAppointment(int ID)
        {

            decimal PaidFees = 0;
            int CreatedByUserID = -1, LocalDrivingLicenseApplicationID = -1, TestTypeID = -1, ReTakeTestApplicationID =-1;
            bool IsLocked = false;
            DateTime AppointmentDate = DateTime.Now;



            if (AppointmentTestAccess.GetAppointmentByID(ID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref PaidFees, ref AppointmentDate, ref CreatedByUserID, ref ReTakeTestApplicationID, ref IsLocked))
            {
                return new clsTestAppintment(ID, TestTypeID, LocalDrivingLicenseApplicationID, PaidFees, AppointmentDate, CreatedByUserID, ReTakeTestApplicationID, IsLocked);

            }
            else
            {
                return null;

            }
        }



        private bool _AddNew()
        {
            this.ID = AppointmentTestAccess.AddAppointmentTest(TestTypeID, LocalDrivingLicenseApplicationID, PaidFees, AppointmentDate, CreatedByUserID, ReTakeTestApplicationID, IsLocked);

            return (this.ID != -1);
        }
        private bool _Update()
        {
            return (AppointmentTestAccess.UpdateAppointment(ID, TestTypeID, LocalDrivingLicenseApplicationID, PaidFees, AppointmentDate, CreatedByUserID, ReTakeTestApplicationID, IsLocked));

        }


        public bool Save()
        {
            switch (Mode)
            {
                case eModes.eNew:
                    if (_AddNew())
                    {
                        Mode = eModes.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eModes.eUpdate:
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
