using BusnissDVLD;
using DVLD_Project.Global;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DVLD_Project
{
    public partial class AddUpdatePeople : Form
    {
        clsPeople People;

        int PersonID;

        public event EventHandler PersonUpdated;

        public enum eModes { eNew = 1, eUpdate = 2 };
        eModes Mode;


        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;



        public AddUpdatePeople()
        {

            InitializeComponent();
            Mode = eModes.eNew;

        }

        public AddUpdatePeople(int PersonID)
        {

            InitializeComponent();

            this.PersonID = PersonID;
            Mode = eModes.eUpdate;

        }


        private void _RefreshDefaultValues()
        {
            _LoadCounties();
            if (Mode == eModes.eNew)
            {
                People = new clsPeople();
                lblTitle.Text = "Add New Person";
            }
            else
            {
                lblTitle.Text = "Update Person";
            }


            if (rbFemale.Checked)
            {
                PersonPicture.Image = Resources.woman;
            }
            else
            {
                PersonPicture.Image = Resources.man;

            }

            if (PersonPicture.ImageLocation != null)
            {
                removeLink.Visible = true;
            }
            else
            {
                removeLink.Visible = false;

            }
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
            CBcountry.SelectedItem = "Jordan";

            txtAddress.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtSecond.Text = "";
            txtThridName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;







        }

        private void _LoadData()
        {
            People = clsPeople.FindPerson(PersonID);
            if (People == null)
            {
                MessageBox.Show("No Person with ID = " + PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = PersonID.ToString();
            txtAddress.Text = People.Address;
            txtEmail.Text = People.Email;
            txtFirstName.Text = People.FirstName;
            txtSecond.Text = People.SecondName;
            txtThridName.Text = People.ThirdName;
            txtLastName.Text = People.LastName;
            txtNationalNo.Text = People.NatoinlNo;
            if (People.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtPhone.Text = People.Phone;

            if (People.ImagePath != "")
            {
                PersonPicture.ImageLocation = People.ImagePath;
                removeLink.Visible = true;

            }
            else
            {
                removeLink.Visible = false;

            }



        }
        private void AddUpdatePeople_Load(object sender, EventArgs e)
        {
            _RefreshDefaultValues();
            if (Mode == eModes.eUpdate)
                _LoadData();
        }
        private void _LoadCounties()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow country in dt.Rows)
            {
                CBcountry.Items.Add(country["CountryName"]);
            }
            CBcountry.SelectedItem = "Jordan";

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (PersonPicture.ImageLocation == null)
                PersonPicture.Image = Resources.woman;
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (PersonPicture.ImageLocation == null)
                PersonPicture.Image = Resources.man;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            removeLink.Visible = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string selectedFilePath = openFileDialog1.FileName;


                PersonPicture.Load(selectedFilePath);

            }

        }


        private void txtPhone_Leave(object sender, EventArgs e)
        {
            string phone = txtPhone.Text;
            if (!clsValidation.ValidateIntger(phone))
            {
                errorProvider1.SetError(txtPhone, "Phone mst be a number");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            }
        }



        private void removeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonPicture.ImageLocation = null;



            if (rbMale.Checked)
                PersonPicture.Image = Resources.man;
            else
                PersonPicture.Image = Resources.woman;

            removeLink.Visible = false;


        }


        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //_Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (People.ImagePath != PersonPicture.ImageLocation)
            {
                if (People.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(People.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (PersonPicture.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = PersonPicture.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        PersonPicture.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_HandlePersonImage())
            {

                return;
            }

            string Country = CBcountry.Text;

            People.FirstName = txtFirstName.Text;
            People.SecondName = txtSecond.Text;
            People.ThirdName = txtThridName.Text;
            People.LastName = txtLastName.Text;
            People.Email = txtEmail.Text;
            People.Phone = txtPhone.Text;

            if (rbFemale.Checked)
            {
                People.Gendor = 1;
            }
            else
            {
                People.Gendor = 0;
            }

            People.NatoinlNo = txtNationalNo.Text;
            People.Address = txtAddress.Text;
            People.CountryInfo = clsCountry.FindCountry(Country);
            People.CountryID = People.CountryInfo.ID;
            People.DateOfBirth = dateTimePicker1.Value;



            if (PersonPicture.ImageLocation != null)
                People.ImagePath = PersonPicture.ImageLocation;
            else
                People.ImagePath = "";


            if (People.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(this, People.ID);
                PersonUpdated?.Invoke(this, EventArgs.Empty);
                if (Mode == eModes.eNew)
                {
                    Mode = eModes.eUpdate;
                    lblTitle.Text = "Update Person";
                    lblPersonID.Text = People.ID.ToString();
                }

            }
            else
            {

                MessageBox.Show("Error in Saving ... ");

            }

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string NationalNo = txtNationalNo.Text.Trim();
            if (string.IsNullOrEmpty(NationalNo))
            {
                errorProvider1.SetError(txtNationalNo, "This Field is required !");
                e.Cancel = true;
            }


            if (clsPeople.IsExist(NationalNo) && People.NatoinlNo != NationalNo)
            {
                errorProvider1.SetError(txtNationalNo, "This NationalNo is used for another person!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);

            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

            string text = txtEmail.Text;
            if (text.Trim() == "")
                return;


            if (!clsValidation.ValidateEmail(text))
            {
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtEmail, "");

            }
        }
    }
}
