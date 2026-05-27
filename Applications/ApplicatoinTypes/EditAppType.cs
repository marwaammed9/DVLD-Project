using BusnissDVLD;
using DVLD_Project.Global;
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
    public partial class EditAppType : Form
    {
        //  public int ID;
        public clsAppType appType;
        public EditAppType(int id)
        {
            InitializeComponent();
            appType = clsAppType.FindAppType(id);

        }

        private void LoadData()
        {
            lblID.Text = appType.ID.ToString();
            txtPrice.Text = appType.Fees.ToString("0.00");
            txtTitle.Text = appType.Name;

        }
        private void EditAppType_Load(object sender, EventArgs e)
        {
            LoadData();
        }
  




        private void btnSave_Click_1(object sender, EventArgs e)
        {
            decimal AppFees = decimal.Parse(txtPrice.Text.Trim());
            string AppTitle = txtTitle.Text.Trim();

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else if (appType.Fees == AppFees && AppTitle == appType.Name)
            {
                MessageBox.Show("Data Saved Suucessfully ..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                appType.Fees = AppFees;
                appType.Name = AppTitle;

                if (appType.Save())
                {
                    MessageBox.Show("Data Saved Successfully ..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Error In Saving ....", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }




        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);

            }
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Price cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);

            }


            if (!clsValidation.IsNumber(txtPrice.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPrice, "Price must be a number!");
            }
            else
            {
                errorProvider1.SetError(txtPrice, "");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
