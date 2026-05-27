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
    public partial class ManageTests : Form
    {
        DataTable dataTable;

        public ManageTests()
        {
            InitializeComponent();
        }

        private void ManageTests_Load(object sender, EventArgs e)
        {
            dataTable = clsTestType.GetAllTests();

            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["TestTypeFees"].DefaultCellStyle.Format = "N2";
            lblRecords.Text = dataTable.Rows.Count.ToString();


            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 120;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 200;

            dataGridView1.Columns[2].HeaderText = "Description";
            dataGridView1.Columns[2].Width = 400;

            dataGridView1.Columns[3].HeaderText = "Fees";
            dataGridView1.Columns[3].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            EditTestType frm = new EditTestType((clsTestType.enTestType)ID);
            frm.ShowDialog();
            ManageTests_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
