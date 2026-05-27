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
    public partial class ManageInternationalApp : Form
    {
        public ManageInternationalApp()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void _RefreshTable()
        {
             dt = clsInternationalLicense.GetInternationalLicense();
            dataGridView1.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }


        private void FilterByComboBox(string FilterBy, string TextToFilter)
        {
            if (TextToFilter.Equals(""))
            {
                _RefreshTable();
            }

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (FilterBy)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":

                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }



            try
            {
                dt.DefaultView.RowFilter = $"{FilterColumn} = {TextToFilter} ";
            }
            catch (Exception ex)
            {

            }


            lblRecords.Text = (dataGridView1.Rows.Count - 1).ToString();

        }
        private void _LoadDate()
        {
            _RefreshTable();
            txtFilter.Visible = false;
            comboBox1.Visible = false;
            CBFilter.SelectedItem = "None";
            comboBox1.SelectedItem = "All";


        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            New_International_License_App frm = new New_International_License_App();
            frm.ShowDialog();
            _RefreshTable();

        }

        private void ManageInternationalApp_Load(object sender, EventArgs e)
        {
            _LoadDate();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Int.License ID";
                dataGridView1.Columns[0].Width = 160;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 150;

                dataGridView1.Columns[2].HeaderText = "Driver ID";
                dataGridView1.Columns[2].Width = 130;

                dataGridView1.Columns[3].HeaderText = "L.License ID";
                dataGridView1.Columns[3].Width = 130;

                dataGridView1.Columns[4].HeaderText = "Issue Date";
                dataGridView1.Columns[4].Width = 180;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 180;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 120;

            }
        }

        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                comboBox1.Visible = false;

            }
            else if (CBFilter.SelectedItem.ToString() == "Is Active")
            {
                txtFilter.Visible = false;
                comboBox1.Visible = true;
            }
            else
            {
                txtFilter.Visible = true;
                comboBox1.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            string TextToFilter = txtFilter.Text;
            FilterByComboBox(FilterBy, TextToFilter);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            if (FilterBy == "Application ID" || FilterBy == "Driver ID" || FilterBy == "International License ID" || FilterBy == "Local License ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }


            }
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            LicneseHistory frm = new LicneseHistory(clsApplication.FindApplicationByID(AppID).AppPersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InterID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            InternationalLicenseInfo frm = new InternationalLicenseInfo(InterID);
            frm.ShowDialog();
        }

        private void ShowPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            Person_Details frm = new Person_Details(clsApplication.FindApplicationByID(AppID).AppPersonID);
            frm.ShowDialog();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = comboBox1.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue != "All")
            {
                //in this case we deal with numbers not string.
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            }
            else
            {
                dt.DefaultView.RowFilter = "";
            }

                lblRecords.Text = (dataGridView1.Rows.Count - 1).ToString();
            }
        }
    }
