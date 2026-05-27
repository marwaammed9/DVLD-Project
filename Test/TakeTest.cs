using BusnissDVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class TakeTest : Form
    {

        int AppointmentID = -1;
        int TestTypeID = -1;
        clsTestAppintment Appointment;
        clsLocalDrivingLicenseApps LocalApp;
        clsTest Test;
        public TakeTest(int AppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            this.AppointmentID = AppointmentID;
            this.TestTypeID = (int)TestTypeID;
        }
        private void LoadData()
        {
            Appointment = clsTestAppintment.FindAppointment(AppointmentID);
            LocalApp = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(Appointment.LocalDrivingLicenseApplicationID);

            if (TestTypeID == 1)
            {
                pictureBox1.Image = Resources.eye_scan;

            }
            else if (TestTypeID == 2)
            {
                pictureBox1.Image = Resources.writing_512x512;

            }
            else if (TestTypeID == 3)
            {
                pictureBox1.Image = Resources.driving_test;

            }


            lblFees.Text = Appointment.PaidFees.ToString();
            lblClass.Text =LocalApp.LicenseClassInfo.className;
            lblLDAppID.Text = LocalApp.LocalID.ToString();
            lblName.Text = LocalApp.ApplicantFullName;

        }



        private void TakeTest_Load(object sender, EventArgs e)
        {
            LoadData();
        }




        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (rbFail.Checked || rbPass.Checked)
            {
                Test = new clsTest();
                Test.Notes = txtNotes.Text;
                if (rbFail.Checked)
                {
                    Test.TestResult = false;
                }
                else
                {
                    Test.TestResult = true;
                }
                Test.CreatedByUserID = GlobalSettings.CurrentUser.UserID;
                Test.TestAppointmentID = AppointmentID;

                if (Test.Save())
                {
                    Appointment.IsLocked = true;
                    lblTestID.Text = Test.TestID.ToString();
                    if (Appointment.Save())
                    {
                        MessageBox.Show("Test Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;

                    }

                }

            }
           
        }

        private void rbPass_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void rbFail_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;

        }
    }
}
