using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsTestType
    {

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestType.enTestType ID { set; get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Fees { get; set; }

        public clsTestType(clsTestType.enTestType ID, string name, decimal fees, string Description)
        {
            this.ID = ID;
            Name = name;
            Fees = fees;
            this.Description = Description;
        }

        public clsTestType()
        {
            this.ID = clsTestType.enTestType.VisionTest;
            this.Name = "";
            this.Fees = -1;
            this.Description = "";
        }

        public static DataTable GetAllTests()
        {
            return TestTypeAccess.GetTestTypes();
        }

        public static clsTestType FindTestType(clsTestType.enTestType ID)
        {
            string Name = "";
            decimal Fees = -1;
            string Des = "";


            if (TestTypeAccess.GetTestTypeByID((int)ID, ref Name, ref Des, ref Fees))
            {
                return new clsTestType(ID, Name, Fees, Des);

            }
            else
            {
                return null;
            }
        }

        private bool _Update()
        {
            return TestTypeAccess.UpdateTestType((int)ID, Name, Description, Fees);
        }
        public bool Save()
        {
            if (_Update())
            {
                return true;
            }
            else
            {
                return false;
            }



        }

    }
}
