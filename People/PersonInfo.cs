using BusnissDVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class PersonInfo : UserControl
    {
        public clsPeople People { get; set; }
        public int PersonID { get; set; }
        private bool _isDefault = true;
        public PersonInfo()
        {
            InitializeComponent();
        }
        public void ResetPersonInfo()
        {
            PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationlNo.Text = "[????]";
            lblName.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pictureBox1.Image = Resources.man;
            linkLabel1.Enabled = false;
            _isDefault = true;

        }
        public bool isDefaultValues()
        {
            return _isDefault;
        }
        public void LoadPerson(int ID)
        {
            People = clsPeople.FindPerson(ID);
            if (People == null)
            {
                MessageBox.Show("No Person with PersonID = " + ID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetPersonInfo();
                return;
            }
            PersonID = ID;
            _FillPersonInfo();
        }
        public void LoadPerson(string NationalNo)
        {
            People = clsPeople.FindPerson(NationalNo);
            if (People == null)
            {
                MessageBox.Show("No Person with NationalNo = " + NationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetPersonInfo();
                return;
            }
            PersonID = People.ID;
            _FillPersonInfo();
        }




        private void _FillPersonInfo()
        {
            linkLabel1.Enabled = true;
            PersonID = People.ID;
            lblPersonID.Text = People.ID.ToString();
            lblNationlNo.Text = People.NatoinlNo;
            lblName.Text = People.FullName;
            lblGendor.Text = People.Gendor == 0 ? "Male" : "Female";
            lblEmail.Text = People.Email;
            lblPhone.Text = People.Phone;
            lblDateOfBirth.Text = People.DateOfBirth.ToShortDateString();
            lblCountry.Text = People.CountryInfo.Name;
            lblAddress.Text = People.Address;
            _LoadPersonImage();
            _isDefault = false;
        }
        private void _LoadPersonImage()
        {

            if (People.Gendor == 0)
            {
                pictureBox1.Image = Resources.man;
            }
            else
            {
                pictureBox1.Image = Resources.woman;
            }
            string ImagePath = People.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBox1.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            AddUpdatePeople frm = new AddUpdatePeople(People.ID);
            frm.PersonUpdated += Frm_PersonUpdated;
            frm.ShowDialog();

        }
        private void Frm_PersonUpdated(object sender, EventArgs e)
        {
            LoadPerson(People.ID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
