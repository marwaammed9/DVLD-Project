using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class RenewLicense : Form
    {
        public RenewLicense()
        {
            InitializeComponent();
        }
        int OldLicenseID = -1;
        clsLicense OldLicense;
        clsLicense NewLicense;



        private bool _CheckID()
        {

            if (clsLicense.IsExist(OldLicenseID))
            {
                if (!OldLicense.IsLicenseExpired())
                {
                    MessageBox.Show("Selected License is not yet Expiared , Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                else if (OldLicense.IsActive == false)
                {
                    MessageBox.Show("Selected License is Not active, Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

            }
            else
            {
                return false;

            }
            return true;

        }
        private void RenewLicense_Load(object sender, EventArgs e)
        {

            filterLicense1.LicenseIDChanged += filterLicense1_LicenseIDChanged;

            lblCreatedID.Text = GlobalSettings.CurrentUser.UserName;
            lblAppFees.Text = clsAppType.FindAppType(2).Fees.ToString();
          //  lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            linkLabelLicenseInfo.Enabled = false;
            linkLabelLicenseHistory.Enabled = false;

        }
        private void filterLicense1_LicenseIDChanged(object sender, EventArgs e)
        {
            OldLicenseID = filterLicense1.LicenseID;

            if (OldLicenseID != -1)
            {
                OldLicense = clsLicense.FindLicenseByID(OldLicenseID);
                if (_CheckID())
                {
                    lbloldLicenseID.Text = OldLicenseID.ToString();
                 
                    lblLicenseFees.Text = OldLicense.LicenseClassIfo.ClassFees.ToString();
                    lblTotalFees.Text = (double.Parse(lblLicenseFees.Text) + double.Parse(lblAppFees.Text)).ToString();
                    lbloldLicenseID.Text = OldLicense.LicenseClassIfo.className;
                    lblExpDate.Text = DateTime.Now.AddYears(OldLicense.LicenseClassIfo.DefaultValidityLenght).ToShortDateString();
                    linkLabelLicenseHistory.Enabled = true;
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
            ShowLicnese frm = new ShowLicnese(NewLicense.ID);
            frm.ShowDialog();

        }

        private void linkLabelLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicneseHistory frm = new LicneseHistory(NewLicense.DriverID);
            frm.ShowDialog();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            NewLicense = OldLicense.RenewLicense(GlobalSettings.CurrentUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            lblAppID.Text = NewLicense.AppID.ToString();

            lblnewID.Text = NewLicense.ID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + NewLicense.ID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnSave.Enabled = false;
            filterLicense1.DisableFilter();
            linkLabelLicenseInfo.Enabled = true;
        }
    }
}
