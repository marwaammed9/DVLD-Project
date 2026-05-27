using BusnissDVLD;
using DVLD_Project.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BusnissDVLD.clsTestType;

namespace DVLD_Project
{
    public partial class EditTestType : Form
    {

        private clsTestType testType;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;


        public EditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            testType = clsTestType.FindTestType(TestTypeID);
            _TestTypeID = TestTypeID;
        }

     

        private void EditTestType_Load(object sender, EventArgs e)
        {
            if (testType != null)
            {
                lblID.Text = ((int)_TestTypeID).ToString();
                txtPrice.Text = testType.Fees.ToString("0.00");
                txtTitle.Text = testType.Name;
                txtDes.Text = testType.Description;

            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Title must have a value");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");

            }

        }

        private void txtDesc_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDes.Text))
            {
                errorProvider1.SetError(txtDes, "Description must have a value");
            }
            else
            {
                errorProvider1.SetError(txtDes, "");

            }
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                errorProvider1.SetError(txtPrice, "Fees must have a value");
            }
            else
            {
                errorProvider1.SetError(txtPrice, "");

            }
            if (!clsValidation.IsNumber(txtPrice.Text))
            {

                errorProvider1.SetError(txtPrice, "Fees not a number");

            }
            else
            {
                errorProvider1.SetError(txtPrice, "");

            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            decimal Fees = decimal.Parse(txtPrice.Text);
            string Name = txtTitle.Text;
            string Des = txtDes.Text;


            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (testType.Fees == Fees && testType.Name == Name && testType.Description == Des)
            {
                MessageBox.Show("Data Saved Suucessfully ..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                testType.Fees = Fees;
                testType.Name = Name;
                testType.Description = Des;

                if (testType.Save())
                {
                    MessageBox.Show("Data Saved Suucessfully ..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error In Saving ....", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
