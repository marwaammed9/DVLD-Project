using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusnissDVLD;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace DVLD_Project
{
    public partial class ManagePeople : Form
    {

        private static DataTable _dtAllPeople = clsPeople.GetPeoples();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");



        //   DataView dv;

        public ManagePeople()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = cbFilter.SelectedItem.ToString();
            string TextToFilter = txtFilter.Text;
            if (string.IsNullOrEmpty(TextToFilter))
            {
                _dtPeople.DefaultView.RowFilter = "";
            }
            else
            {
                FilterByComboBox(FilterBy, TextToFilter);
            }

        }



        private void _RefreshTable()
        {
            _dtAllPeople = clsPeople.GetPeoples();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dataGridViewPeoples.DataSource = _dtPeople;
            lblRecords.Text = _dtAllPeople.Rows.Count.ToString();

        }
        private void _LoadDate()
        {
            _RefreshTable();
            txtFilter.Visible = false;
            cbFilter.SelectedItem = "None";


            if (dataGridViewPeoples.Rows.Count > 0)
            {

                dataGridViewPeoples.Columns[0].HeaderText = "Person ID";
                dataGridViewPeoples.Columns[0].Width = 90;

                dataGridViewPeoples.Columns[1].HeaderText = "National No.";
                dataGridViewPeoples.Columns[1].Width = 90;


                dataGridViewPeoples.Columns[2].HeaderText = "First Name";
                dataGridViewPeoples.Columns[2].Width = 110;

                dataGridViewPeoples.Columns[3].HeaderText = "Second Name";
                dataGridViewPeoples.Columns[3].Width = 120;


                dataGridViewPeoples.Columns[4].HeaderText = "Third Name";
                dataGridViewPeoples.Columns[4].Width = 120;

                dataGridViewPeoples.Columns[5].HeaderText = "Last Name";
                dataGridViewPeoples.Columns[5].Width = 120;

                dataGridViewPeoples.Columns[6].HeaderText = "Gendor";
                dataGridViewPeoples.Columns[6].Width = 100;

                dataGridViewPeoples.Columns[7].HeaderText = "Date Of Birth";
                dataGridViewPeoples.Columns[7].Width = 140;

                dataGridViewPeoples.Columns[8].HeaderText = "Nationality";
                dataGridViewPeoples.Columns[8].Width = 100;


                dataGridViewPeoples.Columns[9].HeaderText = "Phone";
                dataGridViewPeoples.Columns[9].Width = 110;


                dataGridViewPeoples.Columns[10].HeaderText = "Email";
                dataGridViewPeoples.Columns[10].Width = 170;
            }



        }


        private void ManagePeople_Load(object sender, EventArgs e)
        {
            _LoadDate();
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
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }



            if (FilterColumn == "PersonID")
            {

                try
                {
                    _dtPeople.DefaultView.RowFilter = $"{FilterColumn} = {TextToFilter}";
                }
                catch (Exception ex)
                {

                }

            }

            else
            {

                try
                {
                    _dtPeople.DefaultView.RowFilter = $"{FilterColumn} = '{TextToFilter}' ";
                }
                catch (Exception ex)
                {

                }
            }

        }



        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Text = "";
                txtFilter.Focus();

            }
            else
            {
                txtFilter.Visible = false;
                _RefreshTable();


            }


        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = cbFilter.SelectedItem.ToString();
            if (FilterBy == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }


            }
        }

        private void btnAddPeople_Click(object sender, EventArgs e)
        {
            AddUpdatePeople frm = new AddUpdatePeople();
            frm.ShowDialog();
            _RefreshTable();


        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridViewPeoples.CurrentRow.Cells[0].Value;
            AddUpdatePeople frm = new AddUpdatePeople(ID);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Not implemented Yet.");
        }

        private void peopleDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Person_Details frm = new Person_Details((int)dataGridViewPeoples.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewPeoples.CurrentRow.Cells[0].Value;
            if (!clsUser.isUserExistForPersonID(id))
            {
                if (MessageBox.Show("Are You sure to Delete this Person ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (clsPeople.DeletePerson(id))
                    {
                        MessageBox.Show("Person Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _RefreshTable();
                    }
                    else
                    {
                        MessageBox.Show("Faield to Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }
            else
            {
                MessageBox.Show("Person is a User ,Can't to Delete ...", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AddUpdatePeople frm = new AddUpdatePeople();
            frm.ShowDialog();
            _RefreshTable();
        }


    }
}
