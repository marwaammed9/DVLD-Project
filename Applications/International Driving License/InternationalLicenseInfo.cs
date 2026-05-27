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
    public partial class InternationalLicenseInfo : Form
    {
        int intlicenseID = -1;
        public InternationalLicenseInfo(int IntLicenseID)
        {
            InitializeComponent();
            intlicenseID = IntLicenseID;
        }

        private void InternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            clsInternationalLicense inter = clsInternationalLicense.FindInternatioanlByID(intlicenseID);

            lblintLicenseID.Text = intlicenseID.ToString();
            lblExpDate.Text = inter.ExpDate.ToString();
            lblIssuseDate.Text = inter.IssueDate.ToString();

            clsPeople person = clsPeople.FindPerson(inter.AppPersonID);
            lblFullName.Text = inter.ApplicantFullName;
            lblNationalNo.Text = person.NatoinlNo;
            if (person.Gendor == 0)
            {
                lblGendor.Text = "Male";

            }
            else
            {
                lblGendor.Text = "Female";
            }
            lbldriverID.Text = inter.DriverID.ToString();
            lblActive.Text = inter.IsActive.ToString();
            lblLicenseID.Text = inter.IssuedUsingLicenseID.ToString();
            lblDateOfBirth.Text = person.DateOfBirth.ToString();
            lblAppID.Text = inter.AppID.ToString();
            pictureBoxPerson.ImageLocation = person.ImagePath;


        }
    }
}
