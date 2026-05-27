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
    public partial class ManageAppTypes : Form
    {

        DataTable _dtAppType;

        public ManageAppTypes()
        {
            InitializeComponent();

        }
        private void _RefreshTable()
        {
            _dtAppType = clsAppType.GetAllTypes();
            dataGridView1.DataSource = _dtAppType;
            lblRecords.Text = _dtAppType.Rows.Count.ToString();
        }
        private void ManageAppTypes_Load(object sender, EventArgs e)
        {
            _RefreshTable();
            dataGridView1.Columns[2].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 110;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 300;

            dataGridView1.Columns[2].HeaderText = "Fees";
            dataGridView1.Columns[2].Width = 100;


        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            EditAppType frm = new EditAppType(ID);
            frm.ShowDialog();
            _RefreshTable();
        }
    }
}
