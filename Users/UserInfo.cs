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
    public partial class UserInfo : UserControl
    {
        int userID { get; set; } = -1;
        public clsUser user;
        public UserInfo()
        {
            InitializeComponent();

        }
        public UserInfo(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            //        user = clsUser.FindUser(userID);
        }
        private void _ResetPersonInfo()
        {

            personInfo1.ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblisActive.Text = "[???]";
        }

        public void LoadUser(int ID)
        {
            this.userID = ID;
            user = clsUser.FindUser(userID);
            if (user != null)
            {
                personInfo1.LoadPerson(user.PersonID);
                lblUserID.Text = user.UserID.ToString();
                lblUserName.Text = user.UserName.ToString();
                lblisActive.Text = user.isActive ? "yes" : "No";
            }
            else
            {
                _ResetPersonInfo();
                MessageBox.Show($"User with {ID} Not Found", "User", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }



        private void UserInfo_Load(object sender, EventArgs e)
        {
           // LoadUser(userID);
        }
    }
}
