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
    public partial class MainForm : Form
    {
        Login _frmLogin;
        public MainForm(Login _frmLogin)
        {
            InitializeComponent();
            this._frmLogin = _frmLogin;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

 
   


        private void localDrivingLecenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageLocalLinceseApps frm = new ManageLocalLinceseApps();
            frm.ShowDialog();
        }

        private void internationalDrivingLecenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageInternationalApp frm = new ManageInternationalApp();
            frm.ShowDialog();
        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDetaindLicense frm = new ManageDetaindLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Detain_License frm = new Detain_License();
            frm.ShowDialog();
        }



        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageTests frm = new ManageTests();
            frm.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageAppTypes frm = new ManageAppTypes();
            frm.ShowDialog();
        }

        private void peoplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePeople frm = new ManagePeople();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDrivers frm = new ManageDrivers();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUsers manageUsers = new ManageUsers();
            manageUsers.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails frm = new UserDetails(GlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword(GlobalSettings.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalSettings.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void menuStripAccount_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

      

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalSettings.CurrentUser = null;
            _frmLogin.Show();
        }

        private void retakeTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            ManageLocalLinceseApps frm = new ManageLocalLinceseApps();
            frm.ShowDialog();

        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Relase_DetainLicense frm = new Relase_DetainLicense();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddLocalLicense frm = new AddLocalLicense();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            New_International_License_App frm = new New_International_License_App();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RenewLicense frm = new RenewLicense();
            frm.ShowDialog();
        }

        private void replacmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replacment_License frm = new Replacment_License();
            frm.ShowDialog();
        }
    }
}
