using BusnissDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ManageUsers : Form
    {

        private static DataTable _dtAllUsers;

        public ManageUsers()
        {
            InitializeComponent();
        }

        public void _RefreshTable()
        {

            _dtAllUsers = clsUser.GetAllUsers();
            dataGridViewUsers.DataSource = _dtAllUsers;
            lblRecords.Text = _dtAllUsers.Rows.Count.ToString();

        }




        public void _LoadData()
        {
            _RefreshTable();

            CBActive.Visible = false;
            txtFilter.Visible = false;

            CBFilter.SelectedText = "None";

            dataGridViewUsers.Columns[0].HeaderText = "User ID";
            dataGridViewUsers.Columns[0].Width = 110;

            dataGridViewUsers.Columns[1].HeaderText = "Person ID";
            dataGridViewUsers.Columns[1].Width = 120;

            dataGridViewUsers.Columns[2].HeaderText = "Full Name";
            dataGridViewUsers.Columns[2].Width = 350;

            dataGridViewUsers.Columns[3].HeaderText = "UserName";
            dataGridViewUsers.Columns[3].Width = 120;

            dataGridViewUsers.Columns[4].HeaderText = "Is Active";
            dataGridViewUsers.Columns[4].Width = 120;



        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }



        private void CBFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBFilter.SelectedItem.ToString() == "Is Active")
            {
                CBActive.Visible = true;
                txtFilter.Visible = false;
            }
            else if (CBFilter.SelectedItem.ToString() == "None")
            {
                CBActive.Visible = false;
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
                CBActive.Visible = false;

            }
        }

        private void CBActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBActive.SelectedItem.ToString() == "Yes")
            {
                _dtAllUsers.DefaultView.RowFilter = "IsActive = true";

            }
            else if (CBActive.SelectedItem.ToString() == "No")
            {
                _dtAllUsers.DefaultView.RowFilter = "IsActive = false";

            }
            else
            {
                _RefreshTable();
            }
        }


        private void FilterByComboBox(string FilterBy, string TextToFilter)
        {


            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (FilterBy)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;



            }



            if (FilterColumn == "PersonID" || FilterColumn == "UserID")
            {

                try
                {
                    _dtAllUsers.DefaultView.RowFilter = $"{FilterColumn} = {TextToFilter}";
                }
                catch (Exception ex)
                {

                }

            }

            else
            {

                try
                {
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, TextToFilter.Trim());
                }
                catch (Exception ex)
                {

                }
            }



        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            string TextToFilter = txtFilter.Text;

            if (string.IsNullOrEmpty(TextToFilter))
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }
            else
            {
                FilterByComboBox(FilterBy, TextToFilter);
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            string FilterBy = CBFilter.SelectedItem.ToString();
            if (FilterBy == "Person ID" || FilterBy == "User ID")
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }


            }

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUpdateUsers frm = new AddUpdateUsers();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdateUsers frm = new AddUpdateUsers();
            frm.ShowDialog();
            _RefreshTable();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewUsers.CurrentRow.Cells[0].Value;

            AddUpdateUsers frm = new AddUpdateUsers(id);
            frm.ShowDialog();
            _RefreshTable();
        }

        private void peopleDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewUsers.CurrentRow.Cells[0].Value;

            UserDetails frm = new UserDetails(id);
            frm.ShowDialog();
        }

        private void PassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewUsers.CurrentRow.Cells[0].Value;
            ChangePassword frm = new ChangePassword(id);
            frm.ShowDialog();

        }

        private void DeletetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewUsers.CurrentRow.Cells[0].Value;

            if (MessageBox.Show("Are You sure to Delete this User ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(id))
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


        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
