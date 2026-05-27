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
    public partial class UserDetails : Form
    {

       private int UserID = -1;

        public UserDetails(int userID)
        {
            InitializeComponent();
            this.UserID = userID;
        }

        
        private void UserDetails_Load(object sender, EventArgs e)
        {
            userInfo1.LoadUser(UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
