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
using static BusnissDVLD.clsLicense;

namespace DVLD_Project
{
    public partial class Replacment_License : Form
    {
        int OldLicenseID = -1;
        clsLicense OldLicense;
        clsLicense NewLicense;


        clsAppType AppType;
        public Replacment_License()
        {
            InitializeComponent();

        }
        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (radioButtonDamaged.Checked)

                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }
        private bool _CheckID()
        {
            if (OldLicense != null)
            {
                if (OldLicense.IsActive)
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("Selected License is Not active, Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
            }
            else
            {
                return false;
            }

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
                    lblExpDate.Text = DateTime.Now.AddYears(OldLicense.LicenseClassIfo.DefaultValidityLenght).ToShortDateString();
                    linkLabelLicenseHistory.Enabled = true;
                    btnSave.Enabled = true;
                }

            }

        }
        private void Replacment_For_Damaged_Load(object sender, EventArgs e)
        {
            filterLicense1.LicenseIDChanged += filterLicense1_LicenseIDChanged;

            lblCreatedID.Text = GlobalSettings.CurrentUser.UserName;
            lblIssueDate.Text = DateTime.Now.ToShortDateString();

            radioButtonLost.Checked = true;

            linkLabelLicenseHistory.Enabled = false;
            linkLabelLicenseInfo.Enabled = false;

        }

        private void radioButtonDamaged_CheckedChanged(object sender, EventArgs e)
        {
            AppType = clsAppType.FindAppType(4);
            lblTitle.Text = "Replacement For Damaged License";
            lblAppFees.Text = AppType.Fees.ToString();
        }

        private void radioButtonLost_CheckedChanged(object sender, EventArgs e)
        {
            AppType = clsAppType.FindAppType(3);
            lblTitle.Text = "Replacement For Lost License";
            lblAppFees.Text = AppType.Fees.ToString();


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

            NewLicense = OldLicense.Replace(_GetIssueReason(), GlobalSettings.CurrentUser.UserID);
            if (NewLicense != null)
            {
                lblnewID.Text = NewLicense.ID.ToString();
                lblAppID.Text = NewLicense.AppID.ToString();
                MessageBox.Show("Licensed Replaced Successfully with ID=" + NewLicense.ID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                groupBox1.Enabled = false;
                filterLicense1.DisableFilter();
                linkLabelLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;


            }
        }
    }




}
