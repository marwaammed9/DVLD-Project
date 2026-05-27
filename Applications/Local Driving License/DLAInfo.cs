using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class DLAInfo : UserControl
    {
        int LocalID = -1;

        clsLocalDrivingLicenseApps LocalDrivingLicenseApp;

        public DLAInfo()
        {
            InitializeComponent();
        }
        public DLAInfo(int LocalID)
        {
            InitializeComponent();
            this.LocalID = LocalID;
        }

        private void LoadData()
        {
            if (LocalDrivingLicenseApp == null)
            {
              //  MessageBox.Show("No Application with ApplicationID = " + LocalID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            lblApptype.Text = LocalDrivingLicenseApp.AppType.Name;
            lblAppID.Text = LocalDrivingLicenseApp.ID.ToString();
            lblDLAid.Text = LocalID.ToString();

            lblStatus.Text = LocalDrivingLicenseApp.StatusText;


            lblDate.Text = LocalDrivingLicenseApp.AppDate.ToShortDateString();
            lblFees.Text = LocalDrivingLicenseApp.PaidFees.ToString();
            lblCreated.Text = clsUser.FindUser(LocalDrivingLicenseApp.CreatedByUserID).UserName;
            lblStatusDate.Text = LocalDrivingLicenseApp.LastStatusUpdate.ToShortDateString();




            lblFullName.Text = LocalDrivingLicenseApp.ApplicantFullName;
            lblLicenseClass.Text = LocalDrivingLicenseApp.LicenseClassInfo.className;
            lblPassedTest.Text = $"{clsTest.GetPassedTestCount(LocalID)}/3";


            if (clsLicense.FindLicensebyAppID(LocalDrivingLicenseApp.ID) != null)
            {
                linkLabel1.Enabled = true;
            }
            else
            {
                linkLabel1.Enabled = false;

            }


        }

        public void LoadApp(int LocalID)
        {
            this.LocalID = LocalID;

            DLAInfo_Load(null, null);
        }
        private void DLAInfo_Load(object sender, EventArgs e)
        {
            LocalDrivingLicenseApp = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);

            LoadData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense license = clsLicense.FindLicensebyAppID(LocalDrivingLicenseApp.ID);

            ShowLicnese frm = new ShowLicnese(license.ID);
            frm.ShowDialog();

        }

        private void linkLabelPresonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person_Details frm = new Person_Details(LocalDrivingLicenseApp.AppPersonID);
            frm.ShowDialog();

        }
    }
}
