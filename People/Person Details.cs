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
    public partial class Person_Details : Form
    {
        public Person_Details(int ID)
        {
            InitializeComponent();
            personInfo1.LoadPerson(ID);
        }
        public Person_Details(string NationalNo)
        {
            InitializeComponent();
            personInfo1.LoadPerson(NationalNo);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
