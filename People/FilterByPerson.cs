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

namespace DVLD_Project.People
{
    public partial class FilterByPerson : UserControl
    {
        public FilterByPerson()
        {
            InitializeComponent();
        }
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }
        public void FilterFocus()
        {
            txtFilter.Focus();
        }
        private void FilterByPerson_Load(object sender, EventArgs e)
        {
            txtFilter.Focus();
            cbFilter.SelectedIndex = 0;
        }

        private int _PersonID = -1;

        public int PersonID
        {
            get { return personInfo1.PersonID; }
        }

        public clsPeople SelectedPersonInfo
        {
            get { return personInfo1.People; }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
            FindNow();

        }

        private void FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    personInfo1.LoadPerson(int.Parse(txtFilter.Text));

                    break;

                case "National No.":
                    personInfo1.LoadPerson(txtFilter.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(personInfo1.PersonID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Enter text to filter", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddUpdatePeople frm1 = new AddUpdatePeople();
            frm1.DataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFilter.SelectedIndex = 0;
            txtFilter.Text = PersonID.ToString();
            personInfo1.LoadPerson(PersonID);
            OnPersonSelected(PersonID);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilter.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
