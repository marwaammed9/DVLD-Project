using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageDetaindLicense : Form
    {
        public ManageDetaindLicense()
        {
            InitializeComponent();
        }
        DataTable dt;

        private void _RefreshTable()
        {
            dt = clsDetainLicnese.GetDetainLicensesForMain();

            dataGridView1.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();
        }

        private void ManageDetaindLicense_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            txtFilter.Visible = false;
            comboBox1.Visible = false;
            CBFilter.SelectedItem = "None";

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "D.ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "L.ID";
                dataGridView1.Columns[1].Width = 90;

                dataGridView1.Columns[2].HeaderText = "D.Date";
                dataGridView1.Columns[2].Width = 160;

                dataGridView1.Columns[3].HeaderText = "Is Released";
                dataGridView1.Columns[3].Width = 110;


                dataGridView1.Columns[4].HeaderText = "Fine Fees";
                dataGridView1.Columns[4].Width = 110;

                dataGridView1.Columns[5].HeaderText = "Release Date";
                dataGridView1.Columns[5].Width = 160;

                dataGridView1.Columns[6].HeaderText = "N.No.";
                dataGridView1.Columns[6].Width = 90;

                dataGridView1.Columns[7].HeaderText = "Full Name";
                dataGridView1.Columns[7].Width = 330;

                dataGridView1.Columns[8].HeaderText = "Rlease App.ID";
                dataGridView1.Columns[8].Width = 150;
            }

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Relase_DetainLicense frm = new Relase_DetainLicense();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            Detain_License frm = new Detain_License();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBFilter.SelectedItem.ToString() == "Is Released")
            {
                txtFilter.Visible = false;
                comboBox1.Visible = true;
            }
            else if (CBFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                comboBox1.Visible = false;
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
            string TextToFilter = txtFilter.Text.Trim();
            FilterByComboBox(FilterBy, TextToFilter);
        }

        private void FilterByComboBox(string FilterBy, string TextToFilter)
        {
            if (string.IsNullOrEmpty(TextToFilter))
            {
                dt.DefaultView.RowFilter = "";
                return;
            }
            string FilterCol = "";
            switch (FilterBy)
            {
                case "Detain ID":
                    FilterCol = "DetainID";
                    break;


                case "National No.":
                    FilterCol = "NationalNo";
                    break;


                case "Full Name":
                    FilterCol = "FullName";
                    break;

                case "Release Application ID":
                    FilterCol = "ReleaseApplicationID";
                    break;

                default:
                    FilterCol = "None";
                    break;
            }



            if (FilterCol == "NationalNo")
            {

                try
                {
                    dt.DefaultView.RowFilter = $"{FilterCol} = '{TextToFilter}' ";
                }
                catch (Exception ex)
                {

                }

            }
            else if (FilterCol == "FullName")
            {
                dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterCol, TextToFilter);
            }
            else
            {

                try
                {
                    dt.DefaultView.RowFilter = $"{FilterCol} = {TextToFilter} ";
                }
                catch (Exception ex)
                {

                }
            }

            lblRecords.Text = (dataGridView1.Rows.Count - 1).ToString();

        }



        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            if (FilterBy == "DetainID" || FilterBy == "ReleaseApplicationID" || FilterBy == "IsReleased" || FilterBy == "LicenseID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }


            }
        }

        private void ShowPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationlNo = (string)dataGridView1.CurrentRow.Cells[6].Value;
            Person_Details frm = new Person_Details(clsPeople.FindPerson(NationlNo).ID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            ShowLicnese frm = new ShowLicnese(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            LicneseHistory frm = new LicneseHistory(clsDriver.FindDriverByID(clsLicense.FindLicenseByID(LicenseID).DriverID).PersonID);
            frm.ShowDialog();
        }

        private void personDetaindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dataGridView1.CurrentRow.Cells[1].Value;
            Relase_DetainLicense frm = new Relase_DetainLicense(LicenseID);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            int DetainID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            if (clsDetainLicnese.FindDetainLicenseByID(DetainID).IsReleased)
            {
                personDetaindToolStripMenuItem.Enabled = false;
            }
            else
            {
                personDetaindToolStripMenuItem.Enabled = true;

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
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


            if (FilterValue == "All")
                dt.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecords.Text = (dataGridView1.Rows.Count-1).ToString();
        }
        
    }
}
