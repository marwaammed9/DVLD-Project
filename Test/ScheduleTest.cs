using BusnissDVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusnissDVLD.clsTestType;

namespace DVLD_Project
{
    public partial class ScheduleTest : UserControl
    {
        private int LocalID = -1;
        private int AppointmentID = -1;
        private clsLocalDrivingLicenseApps Local;
        private clsTestAppintment Appointemnt;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        clsApplication App;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            groupBox1.Text = "Vision Test";
                            pictureBox1.Image = Resources.eye_scan;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            groupBox1.Text = "Written Test";
                            pictureBox1.Image = Resources.writing_512x512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            groupBox1.Text = "Street Test";
                            pictureBox1.Image = Resources.driving_test;
                            break;


                        }
                }
            }
        }


        public ScheduleTest()
        {
            InitializeComponent();
        }


        public void LoadInfo(int LocalID, int AppointemntID = -1)
        {
            this.LocalID = LocalID;
            this.AppointmentID = AppointemntID;

            if (AppointemntID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }


            Local = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);

            if (Local == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + LocalID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            lblClass.Text = Local.LicenseClassInfo.className;
            lblLDAppid.Text = LocalID.ToString();
            lblFullName.Text = Local.ApplicantFullName;

            lblTrial.Text = Local.TotalTrialsPerTest(_TestTypeID).ToString();


            if (Local.AttendedTest(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;

            }
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;


            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblReFees.Text = clsAppType.FindAppType(7).Fees.ToString();
                groupBox2.Enabled = true;
                lblTilte.Text = "Schedule Retake Test";
                lblReID.Text = "0";
            }
            else
            {
                groupBox2.Enabled = false;
                lblTilte.Text = "Schedule Test";
                lblReFees.Text = "0";
                lblReID.Text = "N/A";
            }


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.FindTestType(_TestTypeID).Fees.ToString();
                dateTimePicker1.MinDate = DateTime.Now;
                lblReID.Text = "N/A";

                Appointemnt = new clsTestAppintment();
            }
            else
            {

                if (!_LoadAppointment())
                    return;
            }
            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblReFees.Text)).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;


        }

        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment
            if (Appointemnt.IsLocked)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Person already Sat for the test, appointment loacked.";
                dateTimePicker1.Enabled = false;
                btnSave.Enabled = false;
                return false;

            }
            else
                lblMessage.Visible = false;

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannot apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.

            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblMessage.Visible = false;

                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.
                    if (!Local.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }


                    return true;

                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    if (!Local.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblMessage.Visible = true;
                        btnSave.Enabled = false;
                        dateTimePicker1.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessage.Visible = false;
                        btnSave.Enabled = true;
                        dateTimePicker1.Enabled = true;
                    }


                    return true;

            }
            return true;

        }
        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                //incase the mode is add new and creation mode is retake test we should create a seperate application for it.
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplication Application = new clsApplication();

                Application.AppPersonID = Local.AppPersonID;
                Application.AppDate = DateTime.Now;
                Application.AppTypeID = 7;
                Application.AppStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusUpdate = DateTime.Now;
                Application.PaidFees = clsAppType.FindAppType(7).Fees;
                Application.CreatedByUserID = GlobalSettings.CurrentUser.UserID;

                if (!Application.Save())
                {
                    Appointemnt.ReTakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Appointemnt.ReTakeTestApplicationID = Application.ID;
                


            }
            return true;
        }

        private bool _LoadAppointment()
        {
            Appointemnt = clsTestAppintment.FindAppointment(AppointmentID);
            if (Appointemnt == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + AppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFees.Text = Appointemnt.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, Appointemnt.AppointmentDate) < 0)
                dateTimePicker1.MinDate = DateTime.Now;
            else
                dateTimePicker1.MinDate = Appointemnt.AppointmentDate;


            dateTimePicker1.Value = Appointemnt.AppointmentDate;
            if (Appointemnt.ReTakeTestApplicationID == -1)
            {
                lblReFees.Text = "0";
                lblReID.Text = "N/A";
                groupBox2.Enabled = false;
            }
            else
            {
                lblReFees.Text = Appointemnt.RetakeTestAppInfo.PaidFees.ToString();
                groupBox2.Enabled = true;
                lblTilte.Text = "Schedule Retake Test";
                lblReID.Text = Appointemnt.RetakeTestAppInfo.ID.ToString();

            }
            return true;


        }

        private void ScheduleTest_Load(object sender, EventArgs e)
        {



        }
        private bool _HandleActiveTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && clsLocalDrivingLicenseApps.IsThereAnActiveScheduledTest(LocalID, _TestTypeID))
            {
                lblMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dateTimePicker1.Enabled = false;
                return false;
            }

            return true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (_HandleRetakeApplication())
            {
                Appointemnt.LocalDrivingLicenseApplicationID = LocalID;
                Appointemnt.CreatedByUserID = GlobalSettings.CurrentUser.UserID;
                Appointemnt.PaidFees = clsTestType.FindTestType(_TestTypeID).Fees;
                Appointemnt.IsLocked = false;
                Appointemnt.TestTypeID = (int)_TestTypeID;
                Appointemnt.AppointmentDate = dateTimePicker1.Value;



                if (Appointemnt.Save())
                {
                    _Mode = enMode.Update;
                    MessageBox.Show("Appointment Saved Succssfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                {
                    MessageBox.Show("error in Saving ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }
    }
}
