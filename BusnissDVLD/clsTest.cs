using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsTest
    {

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public int CreatedByUserID { get; set; }

        public bool TestResult { get; set; }

        public string Notes { get; set; }


        enum eModes { eNew = 1, eUpdate = 2 };
        eModes Mode;




        public clsTest(int testID, int testAppointmentID, int createdByUserID, bool testResult, string notes)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            CreatedByUserID = createdByUserID;
            TestResult = testResult;
            Notes = notes;
            Mode = eModes.eUpdate;
        }

        public clsTest()
        {
            TestID = 0;
            TestAppointmentID = 0;
            CreatedByUserID = 0;
            TestResult = false;
            Notes = "";
            Mode = eModes.eNew;

        }

        public static clsTest FindByID(int ID)
        {
            int TestAppointmentID = 0, CreatedByUserID = 0;
            string Notes = "";
            bool TestResult = false;

            if (TestsAccess.GetTestByTestID(ID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(ID, TestAppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
            {
                return null;
            }

        }

        public static clsTest FindByAppointmentID(int AppointmentID)
        {
            int TestID = 0, CreatedByUserID = 0;
            string Notes = "";
            bool TestResult = false;

            if (TestsAccess.GetTestsByAppointmentID(AppointmentID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, AppointmentID, CreatedByUserID, TestResult, Notes);
            }
            else
            {
                return null;
            }

        }
        public static DataTable GetAllTestks()
        {
            return TestsAccess.GetAllTests();
        }

        private bool _AddNew()
        {
            this.TestID = TestsAccess.AddTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);

            return (this.TestID != -1);
        }
        private bool _Update()
        {
            return (TestsAccess.UpdateTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID));

        }

        public static byte GetPassedTestCount(int localID)
        {
            return (TestsAccess.PassedTestCountByLocalApp(localID));

        }

        public static int GetLastTestByAndTestTypeAndLocalApp(int localID, clsTestType.enTestType _TestTypeID)
        {
            int TestID = -1;
            if (TestsAccess.GetLastTestByAndTestTypeAndLocalApp(localID, (int)_TestTypeID, ref TestID))
            {
                return TestID;
            }
            else
            {
                return -1;
            }
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
