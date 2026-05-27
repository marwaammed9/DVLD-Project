using BusnissDVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.IO;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class DriverLicneseInfo : UserControl
    {
        int DriverID = -1;
        int LicneseID = -1;
        clsDriver driver;
        clsLicense Licnese;

        public DriverLicneseInfo()
        {
            InitializeComponent();
        }
        public DriverLicneseInfo(int LicneseID)
        {
            InitializeComponent();
            this.LicneseID = LicneseID;
        }
        public void LoadData(int LicenseID)
        {

            this.LicneseID = LicenseID;
            if (LicneseID == -1)
            {
                return;
            }
            Licnese = clsLicense.FindLicenseByID(LicneseID);
            driver = clsDriver.FindDriverByID(Licnese.DriverID);
            // clsPeople people = clsPeople.FindPerson(driver.PersonID);

            lblClass.Text = clsLicenseClasses.GetClassByID(Licnese.LicenseClassID).className;
            lblIssuseReason.Text = Licnese.IssueReasonText;
            lbldriverID.Text = driver.DriverID.ToString();
            lblFullName.Text = driver.PersonInfo.FullName;
            if (driver.PersonInfo.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }
            if (Licnese.Notes == "")
            {
                lblNotes.Text = "No Notes";
            }
            else
            {
                lblNotes.Text = Licnese.Notes;
            }

            lblActive.Text = Licnese.IsActive.ToString();

            lblIsDetain.Text = (clsLicense.IsDetain(Licnese.ID) ? "Yes" : "No");
            lblLicenseID.Text = Licnese.ID.ToString();
            lblNationalNo.Text = driver.PersonInfo.NatoinlNo.ToString();
            lblNotes.Text = Licnese.Notes.ToString();
            lblIssuseDate.Text = Licnese.IssuseDate.ToString();
            lblExpDate.Text = Licnese.ExpDate.ToString();
            lblDateOfBirth.Text = driver.PersonInfo.DateOfBirth.ToString();

            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if (Licnese.DriverInfo.PersonInfo.Gendor == 0)
                pictureBox1.Image = Resources.man;
            else
                pictureBox1.Image = Resources.woman;

            string ImagePath = Licnese.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void DriverLicneseInfo_Load(object sender, EventArgs e)
        {
            LoadData(LicneseID);


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
