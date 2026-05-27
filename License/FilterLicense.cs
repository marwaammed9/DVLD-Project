using BusnissDVLD;
using System;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class FilterLicense : UserControl
    {
        public FilterLicense()
        {
            InitializeComponent();
        }
        // عرفي الـ event
        public event EventHandler LicenseIDChanged;

        private int _licneseID { get; set; } = -1;
        public int LicenseID
        {
            get { return _licneseID; }

            set
            {
                _licneseID = value;
                LicenseIDChanged?.Invoke(this, EventArgs.Empty);
            }

        }
        public void DisableFilter()
        {
            groupBox1.Enabled = false;
        }
        public void LicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
        private void FilterLicense_Load(object sender, EventArgs e)
        {
            txtLicenseID.Focus();
        }
        public void LoadLicense(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            btnSearch_Click(null, null);
            DisableFilter();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {

                _licneseID = int.Parse(txtLicenseID.Text);
                if (clsLicense.FindLicenseByID(_licneseID) != null)
                {
                    LicenseID = _licneseID;
                    driverLicneseInfo1.LoadData(LicenseID);
                }
                else
                {
                    MessageBox.Show($"No License With ID {_licneseID}", "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                txtLicenseID.Clear();
                return;
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtLicenseID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                errorProvider1.SetError(txtLicenseID, "LicenseID Requied");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
    }
}
