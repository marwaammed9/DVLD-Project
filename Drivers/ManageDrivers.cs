using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageDrivers : Form
    {
        DataTable table;
        public ManageDrivers()
        {
            InitializeComponent();
        }
        private void _RefreshTable()
        {
            table = clsDriver.GetAllMyDrivers();

            dataGridView1.DataSource = table;
            lblRecords.Text = table.Rows.Count.ToString();

        }

        private void _LoadDate()
        {
            _RefreshTable();
            txtFilter.Visible = false;
            CBFilter.SelectedItem = "None";
        }

        private void FilterByComboBox(string FilterBy, string TextToFilter)
        {
            string FilterColumn = "";

            if (string.IsNullOrEmpty(TextToFilter))
            {
                table.DefaultView.RowFilter = "";
                return;
            }

            switch (FilterBy)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

            }


            if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {

                try
                {
                    table.DefaultView.RowFilter = $"{FilterColumn} = {TextToFilter}";
                }
                catch (Exception ex)
                {

                }

            }

            else
            {

                try
                {
                    table.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, TextToFilter);
                }
                catch (Exception ex)
                {

                }
            }

            lblRecords.Text = (dataGridView1.Rows.Count - 1).ToString();

        }
        private void ManageDrivers_Load(object sender, EventArgs e)
        {
            _LoadDate();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Driver ID";
                dataGridView1.Columns[0].Width = 120;

                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[1].Width = 120;

                dataGridView1.Columns[2].HeaderText = "National No.";
                dataGridView1.Columns[2].Width = 140;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 320;

                dataGridView1.Columns[4].HeaderText = "Date";
                dataGridView1.Columns[4].Width = 170;

                dataGridView1.Columns[5].HeaderText = "Active Licenses";
                dataGridView1.Columns[5].Width = 150;
            }


        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            string TextToFilter = txtFilter.Text.Trim();
            if (FilterBy == "None")
            {
                txtFilter.Visible = false;
                return;
            }
            FilterByComboBox(FilterBy, TextToFilter);
        }

        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBFilter.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;

            }
            else
            {
                txtFilter.Visible = true;
            }

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();

            if (FilterBy == "Person ID" || FilterBy == "Driver ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
