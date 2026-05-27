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
    public partial class ChangePassword : Form
    {

        public int UserID = -1;
        clsUser user;
        //enum EMode { eNew = 1, EUpdate =2}
        //EMode Mode = EMode.EUpdate;

        public ChangePassword(int userID)
        {
            InitializeComponent();
            this.UserID = userID;
            user = clsUser.FindUser(userID);
        }





        private void ChangePassword_Load(object sender, EventArgs e)
        {

            userInfo1.LoadUser(UserID);
            txtCurrentPassword.Focus();

        }





        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Enter All Fields Correctly ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string Pass = txtNewPassword.Text.Trim();

            user.Password = Pass;

            if (user.Save())
            {
                MessageBox.Show("Password Saved Suuccessfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error in saving ...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
            

            if (user.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
            
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
            
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
            ;
        }
    }
}

