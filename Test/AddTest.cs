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
    public partial class AddTest : Form
    {
        int LocalID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int AppointmentID = -1;
        enum eMode { eNew = 1, eUpdate = 2 };
        public AddTest(int LocalID, clsTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();
            this.LocalID = LocalID;
            this.AppointmentID = AppointmentID;
            this._TestTypeID = TestTypeID;
        }


        private void AddTest_Load(object sender, EventArgs e)
        {
            scheduleTest1.TestTypeID = _TestTypeID;
            scheduleTest1.LoadInfo(LocalID, AppointmentID);


        }



        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
