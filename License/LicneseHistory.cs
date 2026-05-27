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
    public partial class LicneseHistory : Form
    {
        int PersonID = -1;
        clsDriver Driver;
        public LicneseHistory(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
        }

        DataTable LocalDt;
        DataTable InterDt;

        private void loadData()
        {
            Driver = clsDriver.FindDriverByPrseonID(this.PersonID);
            if (Driver != null)
            {
                filterByPerson1.LoadPersonInfo(PersonID);
                _LoadInternationalLicenseInfo();
                _LoadLocalLicenseInfo();
            }

        }



        private void LicneseHistory_Load(object sender, EventArgs e)
        {
            if (PersonID != -1)
            {
                filterByPerson1.FilterEnabled = false;
                loadData();
            }
            else
            {
                filterByPerson1.FilterEnabled = true;

            }


        }
        private void _LoadInternationalLicenseInfo()
        {

            InterDt = clsDriver.GetInternationalLicenses(Driver.DriverID);


            dataGridViewInter.DataSource = InterDt;
            lblInterRecords.Text = InterDt.Rows.Count.ToString();

            if (dataGridViewInter.Rows.Count > 0)
            {
                dataGridViewInter.Columns[0].HeaderText = "Inter.ID";
                dataGridViewInter.Columns[0].Width = 90;

                dataGridViewInter.Columns[1].HeaderText = "Application ID";
                dataGridViewInter.Columns[1].Width = 90;

                dataGridViewInter.Columns[2].HeaderText = "L.License ID";
                dataGridViewInter.Columns[2].Width = 90;

                dataGridViewInter.Columns[3].HeaderText = "Issue Date";
                dataGridViewInter.Columns[3].Width = 170;

                dataGridViewInter.Columns[4].HeaderText = "Expiration Date";
                dataGridViewInter.Columns[4].Width = 170;

                dataGridViewInter.Columns[5].HeaderText = "Is Active";
                dataGridViewInter.Columns[5].Width = 90;

            }
        }
        private void _LoadLocalLicenseInfo()
        {

            LocalDt = clsDriver.GetLicenses(Driver.DriverID);


            dataGridView1.DataSource = LocalDt;
            lblLocalRecords.Text = LocalDt.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Lic.ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "App.ID";
                dataGridView1.Columns[1].Width = 90;

                dataGridView1.Columns[2].HeaderText = "Class Name";
                dataGridView1.Columns[2].Width = 110;

                dataGridView1.Columns[3].HeaderText = "Issue Date";
                dataGridView1.Columns[3].Width = 170;

                dataGridView1.Columns[4].HeaderText = "Expiration Date";
                dataGridView1.Columns[4].Width = 170;

                dataGridView1.Columns[5].HeaderText = "Is Active";
                dataGridView1.Columns[5].Width = 90;

            }
        }


        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;


            ShowLicnese frm = new ShowLicnese(id);
            frm.ShowDialog();
        }
    }
}
