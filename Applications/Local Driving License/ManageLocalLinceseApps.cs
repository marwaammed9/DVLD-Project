using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageLocalLinceseApps : Form
    {
        DataTable table;


        public ManageLocalLinceseApps()
        {
            InitializeComponent();
        }
        private void _RefreshTable()
        {
            table = clsLocalDrivingLicenseApps.GetAllLocalApps();
            dataGridView1.DataSource = table;
            lblRecords.Text = table.Rows.Count.ToString();

        }
        private void ManageLocalLinceseApps_Load(object sender, EventArgs e)
        {

            _LoadDate();

        }

        private void _LoadDate()
        {
            _RefreshTable();
            txtFilter.Visible = false;


            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView1.Columns[0].HeaderText = "L.D.L.AppID";
                dataGridView1.Columns[0].Width = 120;

                dataGridView1.Columns[1].HeaderText = "Driving Class";
                dataGridView1.Columns[1].Width = 300;

                dataGridView1.Columns[2].HeaderText = "National No.";
                dataGridView1.Columns[2].Width = 150;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 350;

                dataGridView1.Columns[4].HeaderText = "Application Date";
                dataGridView1.Columns[4].Width = 170;

                dataGridView1.Columns[5].HeaderText = "Passed Tests";
                dataGridView1.Columns[5].Width = 150;

                dataGridView1.Columns[6].HeaderText = "Status";
                dataGridView1.Columns[6].Width = 170;
            }

            CBFilter.SelectedItem = "None";

        }

        private void FilterByComboBox(string FilterBy, string TextToFilter)
        {

            string FilterCol = "";

            switch (FilterBy)
            {
                case "Full Name":
                    FilterCol = "FullName";
                    break;

                case "National No.":
                    FilterCol = "NationalNo";
                    break;

                case "L.D.L.AppID":
                    FilterCol = "LocalDrivingLicenseApplicationID";
                    break;

                case "Status":
                    FilterCol = "Status";
                    break;

            }



            if (TextToFilter.Equals(""))
            {
                table.DefaultView.RowFilter = "";
                lblRecords.Text = table.Rows.Count.ToString();
            }

            else
            {

                if (FilterCol == "LocalDrivingLicenseApplicationID")
                {

                    try
                    {
                        table.DefaultView.RowFilter = $"{FilterCol} = {TextToFilter}";

                    }
                    catch (Exception ex)
                    {

                    }

                }
                else if (FilterCol == "NationalNo")
                {
                    try
                    {
                        table.DefaultView.RowFilter = $"{FilterCol} = '{TextToFilter}' ";

                    }
                    catch (Exception ex)
                    {

                    }
                }

                else
                {

                    try
                    {
                        table.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterCol, TextToFilter.Trim());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                lblRecords.Text = (dataGridView1.Rows.Count - 1).ToString();

            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            string TextToFilter = txtFilter.Text;

            FilterByComboBox(FilterBy, TextToFilter);




        }

        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if (CBFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;

            }
            else txtFilter.Visible = true;

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            if (FilterBy == "L.D.L.AppID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }


            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddLocalLicense frm = new AddLocalLicense();
            frm.ShowDialog();
            _RefreshTable();
        }


        private void AppDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            LocalAppDetails frm = new LocalAppDetails(LocalID);
            frm.ShowDialog();

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            AddLocalLicense frm = new AddLocalLicense(LocalID);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sechdualVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ManageTestAppointment frm = new ManageTestAppointment(LocalID, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void sechdualWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            ManageTestAppointment frm = new ManageTestAppointment(LocalID, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            _RefreshTable();

        }
        private void ReloadMenu(int LocalID)
        {
            clsLocalDrivingLicenseApps local = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);
            int TotalPassedTests = (int)dataGridView1.CurrentRow.Cells[5].Value;
            bool LicenseExsit = false;

            if (clsLicense.getActiveLicensebyClassIDAndPersonID(local.ClassID, local.AppPersonID) == -1)
            {
                LicenseExsit = false;
            }
            else
            {
                LicenseExsit = true;
            }

            TestsToolStripMenuItem.Enabled = (TotalPassedTests != 3) && !(local.AppStatus == clsApplication.enApplicationStatus.Cancelled);

            IssuseLicenseStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExsit;
            sechdualVisionTestToolStripMenuItem.Enabled = (TotalPassedTests == 0);
            sechdualWrittenTestToolStripMenuItem.Enabled = (TotalPassedTests == 1);
            sechdualStreetTestToolStripMenuItem.Enabled = (TotalPassedTests == 2);

            updateToolStripMenuItem.Enabled = (!LicenseExsit && TotalPassedTests != 3);
            deleteToolStripMenuItem.Enabled = (!LicenseExsit && TotalPassedTests != 3);
            CnacleAppToolStripMenuItem.Enabled = (!LicenseExsit && TotalPassedTests != 3);
            showLicenseToolStripMenuItem.Enabled = LicenseExsit;
            showPersonLicensesHistoryToolStripMenuItem.Enabled = clsPeople.IsDriver(local.AppPersonID);




        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ReloadMenu(LocalID);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApps LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _RefreshTable();
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void IssuseLicenseStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            Issuse_Driver_License_For_the_First_Time frm = new Issuse_Driver_License_For_the_First_Time(LocalID);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApps local = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);

            ShowLicnese frm = new ShowLicnese(clsLicense.getActiveLicensebyClassIDAndPersonID(local.ClassID, local.AppPersonID));
            frm.ShowDialog();

        }

        private void CnacleAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApps LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _RefreshTable();
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApps localDrivingLicenseApplication = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalDrivingLicenseApplicationID);

            LicneseHistory frm = new LicneseHistory(localDrivingLicenseApplication.AppPersonID);
            frm.ShowDialog();
        }

        private void AppDetailesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;

            LocalAppDetails frm = new LocalAppDetails(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void sechdualStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            ManageTestAppointment frm = new ManageTestAppointment(LocalDrivingLicenseApplicationID,clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            _RefreshTable();
        }
    }
}







