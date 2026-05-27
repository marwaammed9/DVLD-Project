using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class Relase_DetainLicense : Form
    {
        int LicenseID = -1;

        clsDetainLicnese DeLicense;

        public Relase_DetainLicense(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }
        public Relase_DetainLicense()
        {
            InitializeComponent();

        }
        private bool _CheckID()
        {

            if (clsLicense.IsExist(LicenseID))
            {

                if (!clsLicense.IsDetain(LicenseID))
                {
                    MessageBox.Show("Selected License is Not Detaind, Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

            }
            else
            {
                return false;

            }
            return true;

        }
        private void Relase_DetainLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToString();
            lblCreatedID.Text = GlobalSettings.CurrentUser.UserName;
            lblAppFees.Text = clsAppType.FindAppType(5).Fees.ToString();
            linkLabelLicenseHistory.Enabled = false;
            linkLabelLicenseInfo.Enabled = false;
            if (LicenseID != -1)
            {
                filterLicense1.LicenseID = LicenseID;
                filterLicense1.LoadLicense(LicenseID);
                filterLicense1.DisableFilter();
            }

            filterLicense1.LicenseIDChanged += filterLicense1_LicenseIDChanged;


        }



        private void filterLicense1_LicenseIDChanged(object sender, EventArgs e)
        {

            LicenseID = filterLicense1.LicenseID;
            if (LicenseID != -1)
            {
                if (_CheckID())
                {
                    DeLicense = clsDetainLicnese.FindDetainLicensebyLicenseID(LicenseID);
                    lblReLicenseID.Text = LicenseID.ToString();
                    lblDetainID.Text = DeLicense.ID.ToString();
                    lblFineFees.Text = DeLicense.FineFees.ToString();
                    lblTotalFees.Text = (decimal.Parse(lblFineFees.Text) + decimal.Parse(lblAppFees.Text)).ToString();
                    linkLabelLicenseInfo.Enabled = true;
                    btnSave.Enabled = true;

                }
                else
                {
                    btnSave.Enabled = false;
                }
            }
        }

        private void linkLabelLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicnese frm = new ShowLicnese(LicenseID);
            frm.ShowDialog();
        }

        private void linkLabelLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicneseHistory frm = new LicneseHistory(clsDriver.FindDriverByID(clsLicense.FindLicenseByID(LicenseID).DriverID).PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int AppID = -1;
            bool IsReleased = clsLicense.FindLicenseByID(LicenseID).ReleaseDetainedLicense(GlobalSettings.CurrentUser.UserID, ref AppID);
            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnSave.Enabled = false;
            filterLicense1.DisableFilter();
            //   linkLabelLicenseInfo.Enabled = true;
            lblAppID.Text = AppID.ToString();
            linkLabelLicenseHistory.Enabled = true;
        }
    }
}
