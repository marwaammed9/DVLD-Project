using BusnissDVLD;
using DVLD_Project.Properties;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        clsUser user;
        private void Login_Load(object sender, EventArgs e)
        {

            string UserName = "", Password = "";

            if (GlobalSettings.GetStoredCredential(ref UserName, ref Password))
            {
                txtLogin.Text = UserName;
                txtPass.Text = Password;
                checkBox1.Checked = true;
            }
            else
                checkBox1.Checked = false;
        }



        private void btnNext_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindUser(txtLogin.Text, txtPass.Text);
            if (user != null)
            {
                if (!user.isActive)
                {

                    txtLogin.Focus();
                    MessageBox.Show("Your account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBox1.Checked)
                {
                    //store username and password
                    GlobalSettings.RememberUsernameAndPassword(txtLogin.Text.Trim(), txtPass.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    GlobalSettings.RememberUsernameAndPassword("", "");

                }

                GlobalSettings.CurrentUser = user;

                this.Hide();
                MainForm frm = new MainForm(this);
                frm.ShowDialog();
         //       this.Close();


            }
            else
            {
                txtLogin.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
