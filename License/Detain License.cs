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

    public partial class Detain_License : Form
    {
        int LicenseID = -1;
        clsLicense license;
        clsDetainLicnese DetainLicnese;


        public Detain_License()
        {
            InitializeComponent();
        }

        private bool _CheckID()
        {

            if (clsLicense.IsExist(LicenseID))
            {

                if (clsLicense.FindLicenseByID(LicenseID).IsActive == false)
                {
                    MessageBox.Show("Selected License is Not active, Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                if (clsLicense.IsDetain(LicenseID))
                {
                    MessageBox.Show("Selected License is Alraedy Detaind, Enter another License ", "Show", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

            }
            else
            {
                return false;

            }
            return true;

        }

        private void Detain_License_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToString();
            lblCreatedID.Text = GlobalSettings.CurrentUser.UserName;
            linkLabelLicenseHistory.Enabled = false;
            linkLabelLicenseInfo.Enabled = false;
            filterLicense1.LicenseIDFocus();
            filterLicense1.LicenseIDChanged += filterLicense1_LicenseIDChanged_1;

        }







        private void filterLicense1_LicenseIDChanged_1(object sender, EventArgs e)
        {
            this.LicenseID = filterLicense1.LicenseID;


            if (_CheckID())
            {
                lblLicenseID.Text = LicenseID.ToString();
                license = clsLicense.FindLicenseByID(LicenseID);
                //    linkLabelLicenseHistory.Enabled = true;
                linkLabelLicenseInfo.Enabled = true;
                btnSave.Enabled = true;
              //  textBox1.Focus();
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void linkLabelLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicnese frm = new ShowLicnese(LicenseID);
            frm.ShowDialog();
        }

        private void linkLabelLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicneseHistory frm = new LicneseHistory(clsDriver.FindDriverByID(license.DriverID).PersonID);
            frm.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (license != null)
                {
                    if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                    int DetainID = license.Detain(decimal.Parse(textBox1.Text), GlobalSettings.CurrentUser.UserID);
                    if (DetainID == -1)
                    {
                        MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    lbDetainID.Text = DetainID.ToString();
                    MessageBox.Show("License Detained Successfully with ID=" + DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnSave.Enabled = false;
                    filterLicense1.DisableFilter();
                    textBox1.Enabled = false;
                    linkLabelLicenseHistory.Enabled = true;

                }
            }
            
        }

       
    }
}
