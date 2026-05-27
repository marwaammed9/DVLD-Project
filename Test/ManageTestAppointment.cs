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
    public partial class ManageTestAppointment : Form
    {


        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;


        public ManageTestAppointment(int LocalID, clsTestType.enTestType _TestType)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LocalID;
            this._TestType = _TestType;
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTilte.Text = "Vision Test Appointments";
                        this.Text = lblTilte.Text;
                        pictureBox1.Image = Resources.eye_scan;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTilte.Text = "Written Test Appointments";
                        this.Text = lblTilte.Text;
                        pictureBox1.Image = Resources.writing_512x512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTilte.Text = "Street Test Appointments";
                        this.Text = lblTilte.Text;
                        pictureBox1.Image = Resources.driving_test;
                        break;
                    }
            }
        }

        private void _RevreshTable()
        {
            _dtLicenseTestAppointments = clsTestAppintment.GetAllTestAppointmentsWith_LocalIDAndTestType(_LocalDrivingLicenseApplicationID, (int)_TestType);
            dataGridView1.DataSource = _dtLicenseTestAppointments;
            lblRecords.Text = _dtLicenseTestAppointments.Rows.Count.ToString();
        }


        private void btnAddApppintment_Click(object sender, EventArgs e)
        {

            clsTest LastTest = clsTest.FindByID(clsTest.GetLastTestByAndTestTypeAndLocalApp(_LocalDrivingLicenseApplicationID, _TestType));

            if (LastTest == null)
            {
                AddTest frm = new AddTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm.ShowDialog();
                _RevreshTable();
                return;

            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddTest frm1 = new AddTest(_LocalDrivingLicenseApplicationID, _TestType);
            frm1.ShowDialog();
            _RevreshTable();
            return;

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            AddTest frm = new AddTest(_LocalDrivingLicenseApplicationID, _TestType, AppointmentID);
            frm.ShowDialog();
            _RevreshTable();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            if (clsTestAppintment.FindAppointment(TestAppointmentID).IsLocked == false)
            {
                TakeTest frm = new TakeTest(TestAppointmentID, _TestType);
                frm.ShowDialog();
                _RevreshTable();
            }
            else
            {

                MessageBox.Show("This Appointment is Locked", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ManageTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            _RevreshTable();

            dlaInfo1.LoadApp(_LocalDrivingLicenseApplicationID);

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Appointment ID";
                dataGridView1.Columns[0].Width = 150;

                dataGridView1.Columns[1].HeaderText = "Appointment Date";
                dataGridView1.Columns[1].Width = 200;

                dataGridView1.Columns[2].HeaderText = "Paid Fees";
                dataGridView1.Columns[2].Width = 150;

                dataGridView1.Columns[3].HeaderText = "Is Locked";
                dataGridView1.Columns[3].Width = 100;
            }

        }
    }
}
