using BusnissDVLD;
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
    public partial class New_International_License_App : Form
    {
        public New_International_License_App()
        {
            InitializeComponent();
        }

        clsInternationalLicense interLicense;
        clsLicense license;
        int LicenseID = -1;


        private void New_International_License_App_Load(object sender, EventArgs e)
        {
            lblApptype.Text = clsAppType.FindAppType(6).Name;
            lblCreated.Text = GlobalSettings.CurrentUser.UserName;
            lblDate.Text = DateTime.Now.ToString();
            lblFees.Text = clsAppType.FindAppType(6).Fees.ToString();
            filterLicense1.LicenseIDChanged += FilterLicense1_LicenseIDChanged;



        }

        private void FilterLicense1_LicenseIDChanged(object sender, EventArgs e)
        {
            LicenseID = filterLicense1.LicenseID;
            license = clsLicense.FindLicenseByID(LicenseID);

            if (LicenseID != -1)
            {
                if (clsInternationalLicense.GetInternationalLicenseByDriverID(license.DriverID) != -1)
                {
                    MessageBox.Show("This Driver Has an active Internatioanl License", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (clsLicense.FindLicenseByID(LicenseID).LicenseClassID != 3)
                {
                    MessageBox.Show("Internatoial License should be Class 3", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!clsLicense.FindLicenseByID(LicenseID).IsActive)
                {
                    MessageBox.Show("This License is not Active", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                lblFullName.Text = clsDriver.FindDriverByID(license.DriverID).PersonID.ToString();
                btnSave.Enabled = true;
                linkLabelLicenseInfo.Enabled = true;

            }
        }

        private void linkLabelLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalLicenseInfo frm = new InternationalLicenseInfo(interLicense.InterLicenseID);
            frm.ShowDialog();
        }

        private void linkLabelLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicneseHistory frm = new LicneseHistory(license.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linkLabelPresonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Person_Details frm = new Person_Details(interLicense.AppPersonID);
            frm.ShowDialog();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            interLicense = new clsInternationalLicense();
            interLicense.PaidFees = decimal.Parse(lblFees.Text);
            interLicense.LastStatusUpdate = DateTime.Now;
            interLicense.IssueDate = DateTime.Now;
            interLicense.AppDate = DateTime.Now;
            interLicense.AppTypeID = 6;
            interLicense.IssuedUsingLicenseID = LicenseID;
            interLicense.AppPersonID = license.DriverInfo.PersonID;
            interLicense.DriverID = license.DriverID;
            interLicense.ExpDate = DateTime.Now.AddYears(1);
            interLicense.AppStatus = clsApplication.enApplicationStatus.Completed;
            interLicense.CreatedByUserID = GlobalSettings.CurrentUser.UserID;

            if (!interLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblInterID.Text = interLicense.InterLicenseID.ToString();
            lblAppID.Text = interLicense.ID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + interLicense.InterLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = false;
            linkLabelLicenseHistory.Enabled = true;
            filterLicense1.DisableFilter();


        }
    }
}
