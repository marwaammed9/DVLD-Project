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
    public partial class Issuse_Driver_License_For_the_First_Time : Form
    {
        int LocalID;
        clsLocalDrivingLicenseApps local;
        public Issuse_Driver_License_For_the_First_Time(int LocalID)
        {
            InitializeComponent();
            this.LocalID = LocalID;
        }



        private void Issuse_Driver_License_For_the_First_Time_Load(object sender, EventArgs e)
        {

            txtNotes.Focus();

            local = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);

            if (local == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + LocalID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!local.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = local.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }
            dlaInfo1.LoadApp(LocalID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = local.IssueLicenseForTheFirtTime(txtNotes.Text, GlobalSettings.CurrentUser.UserID);
            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
