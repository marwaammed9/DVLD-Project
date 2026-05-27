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
    public partial class ShowLicnese : Form
    {

        int LicenseID = -1;

       
        public ShowLicnese(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }



        private void ShowLicnese_Load(object sender, EventArgs e)
        {
            driverLicneseInfo1.LoadData(LicenseID);
        }

  

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
