using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class AddUpdateUsers : Form
    {

        int PersonID = -1;
        clsUser user;
        int UserID = -1;
        enum eModes { eNew = 1, eUpdate = 2 };
        eModes Mode;


        public AddUpdateUsers()
        {
            InitializeComponent();
            Mode = eModes.eNew;

        }

        public AddUpdateUsers(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            this.user = clsUser.FindUser(UserID);
            this.PersonID = user.PersonID;
            Mode = eModes.eUpdate;


        }

        public void _LoadDate()
        {


            if (Mode == eModes.eNew)
            {

                lblTitle.Text = "Add User";
                user = new clsUser();
                return;
            }

            lblTitle.Text = "Update User";


            if (user == null)
            {
                MessageBox.Show("This Form will Be Closed");
                this.Close();
                return;
            }


            filterByPerson1.LoadPersonInfo(user.PersonID);
            filterByPerson1.FilterEnabled = false;

            txtUsername.Text = user.UserName;
            txtPass.Text = user.Password;
            txtConiformPass.Text = user.Password;
            lblUserID.Text = user.UserID.ToString();
            if (user.isActive == true)
            {
                CHBisActive.Checked = true;
            }
            else
            {

                CHBisActive.Checked = false;
            }
                btnNext.Enabled = true;


        }

        private void AddUpdateUsers_Load(object sender, EventArgs e)
        {
            _LoadDate();
            filterByPerson1.OnPersonSelected += FilterPeople1_OnPersonSelected;
        }
        private void FilterPeople1_OnPersonSelected(int PersonID)
        {
            if (clsUser.isUserExistForPersonID(PersonID))
            {
                MessageBox.Show("Selected Person is already has a user , Select Another One", "ADD User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filterByPerson1.FilterFocus();
                btnNext.Enabled = false;
                return;

            }
            else if (!clsPeople.IsExist(PersonID))
            {
                btnNext.Enabled = false;
                return;
            }
            btnNext.Enabled = true;
            this.PersonID = PersonID;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 1;
            tbLoginInfo.Enabled = true;
            txtUsername.Focus();
            btnSave.Enabled = true;

        }


        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPass, "Password Must have a value");

            }
            else
            {
                errorProvider1.SetError(txtPass, null);
            }
        }

        private void txtConiformPass_Validating(object sender, CancelEventArgs e)
        {
            if (txtConiformPass.Text == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConiformPass, "Password Must have a value");

            }
            else if (txtConiformPass.Text != txtPass.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConiformPass, "Password Must Match");

            }
            else
            {
                errorProvider1.SetError(txtConiformPass, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            user.Password = txtPass.Text.Trim();
            user.UserName = txtUsername.Text.Trim();
            user.PersonID = PersonID;
            user.isActive = CHBisActive.Checked;

            if (user.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                lblTitle.Text = "Update User";
                lblUserID.Text = user.UserID.ToString();
                Mode = eModes.eUpdate;
            }
            else
            {
                MessageBox.Show("Error in Saving ....", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }



        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            }



            if (Mode == eModes.eNew)
            {

                if (clsUser.IsExist(txtUsername.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUsername, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUsername, null);
                }

            }
            else
            {
                //incase update make sure not to use anothers user name
                if (user.UserName != txtUsername.Text.Trim())
                {
                    if (clsUser.IsExist(txtUsername.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUsername, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, null);
                    }

                }

            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnNext.Enabled)
                tbLoginInfo.Enabled = true;
            else
                tbLoginInfo.Enabled = false;
        }


    }
}
