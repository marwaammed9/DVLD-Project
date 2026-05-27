using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace DVLD_Project
{
    public partial class AddLocalLicense : Form
    {
        int LocalID;

        enum eModes { eNew = 1, eUpdate }
        eModes mode = eModes.eNew;

        public AddLocalLicense()
        {
            InitializeComponent();
            mode = eModes.eNew;

        }
        public AddLocalLicense(int LocalID)
        {
            InitializeComponent();
            this.LocalID = LocalID;
            mode = eModes.eUpdate;

        }

        int personID = -1;
        clsPeople person;


        clsLocalDrivingLicenseApps LDL;

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClasses.getClasses();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                comboBox1.Items.Add(row["ClassName"]);
            }
        }

        public void _LoadDate()
        {



            if (mode == eModes.eUpdate)
            {
                lblTitle.Text = "Update Local License Application";
                LDL = clsLocalDrivingLicenseApps.FindLocalAppByLocalID(LocalID);
                if (LDL == null)
                {
                    MessageBox.Show("No Application with ID = " + LocalID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();

                    return;
                }

                person = clsPeople.FindPerson(LDL.AppPersonID);
                filterByPerson1.LoadPersonInfo(person.ID);
                lblAppID.Text = LDL.ID.ToString();
                lblAppDate.Text = LDL.AppDate.ToString();
                lblAppFees.Text = LDL.PaidFees.ToString();
                tabPage2.Enabled = true;
                btnNext.Enabled = true;
                btnSave.Enabled = true;
                filterByPerson1.FilterEnabled = false;
                lblUser.Text = LDL.CreatedByUser.UserName;
                comboBox1.SelectedItem = LDL.LicenseClassInfo.className;


            }
            else
            {

                lblTitle.Text = "Add New Local License Application";
                lblAppDate.Text = DateTime.Now.ToShortDateString();
                filterByPerson1.FilterFocus();
                tabPage2.Enabled = false;
                comboBox1.SelectedIndex = 2;
                lblAppFees.Text = clsAppType.FindAppType(1).Fees.ToString();
                lblUser.Text = GlobalSettings.CurrentUser.UserName;
                LDL = new clsLocalDrivingLicenseApps();
            }



        }
        private void AddLocalLicense_Load(object sender, EventArgs e)
        {
            _FillLicenseClassesInComoboBox();
            _LoadDate();
            filterByPerson1.OnPersonSelected += PersonCardWithFilter1_OnPersonSelected;
        }

        private void PersonCardWithFilter1_OnPersonSelected(int PersonID)
        {
            this.personID = PersonID;
            person = clsPeople.FindPerson(PersonID);
            btnNext.Enabled = true;
         
            tabPage2.Enabled = true;


        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }



        private void btnNext_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 1;
            btnSave.Enabled = true;

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            clsAppType AppType = clsAppType.FindAppType(1);

            int UserID = GlobalSettings.CurrentUser.UserID;
            int classID = clsLicenseClasses.GetClassByName(comboBox1.SelectedItem.ToString()).ID;



            if (personID != -1)
            {
                if (clsLocalDrivingLicenseApps.GetActiveApplicationIDForLicenseClass(personID, 1, classID) != -1)
                {
                    MessageBox.Show("Choose Another License Class , The Selected person alraedy have an active Application For this Class ... ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;
                }
                if (clsLicense.getActiveLicensebyClassIDAndPersonID(personID, classID) != -1)
                {
                    MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboBox1.Focus();
                    return;


                }

                LDL.PaidFees = decimal.Parse(lblAppFees.Text);
                LDL.AppDate = DateTime.Now;
                LDL.ClassID = classID;
                LDL.AppType = AppType;
                LDL.AppTypeID = AppType.ID;
                LDL.AppStatus = clsApplication.enApplicationStatus.New;
                LDL.AppPersonID = personID;
                LDL.CreatedByUserID = UserID;
                LDL.LastStatusUpdate = DateTime.Now;



                if (LDL.Save())
                {
                    lblAppID.Text = LDL.ID.ToString();
                    mode = eModes.eUpdate;
                    lblTitle.Text = "Update Local License Application";
                    MessageBox.Show("Data Saved Successfully .. ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error in Saving ... ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
