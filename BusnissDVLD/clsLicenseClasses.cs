using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsLicenseClasses
    {

        public int ID { get; set; }

        public string className { get; set; }

        public string ClassDescription { get; set; }

        public byte MinimumAllowedAge { get; set; }

        public byte DefaultValidityLenght { get; set; }

        public decimal ClassFees { get; set; }

        public clsLicenseClasses()
        {
            ID = -1;
            className = "";
            ClassDescription = "";
            MinimumAllowedAge = 0;
            DefaultValidityLenght = 0;
            ClassFees = 0;

        }

        public clsLicenseClasses(int ID, string ClassName, string Description, byte DefaultValidityLenght, byte MinimumAllowedAge, decimal ClassFees)
        {
            this.ID = ID;
            this.className = ClassName;
            this.ClassDescription = Description;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLenght = DefaultValidityLenght;
            this.ClassFees = ClassFees;

        }

        public static DataTable getClasses()
        {
            return LicenseClassesAccess.GetClassLicense();
        }

        public static clsLicenseClasses GetClassByName(string Name)
        {
            int ID = -1;
            string ClassDescription = "";
            byte MinAllowedAge = 0;
            byte DefaultValidityLength = 0;
            decimal ClassFees = -1;

            if (LicenseClassesAccess.GetClassByName(Name, ref ID, ref ClassDescription, ref MinAllowedAge, ref DefaultValidityLength, ref ClassFees)){
                return  new clsLicenseClasses(ID, Name, ClassDescription, DefaultValidityLength, MinAllowedAge, ClassFees);
            }
            else
            {
                return null;
            }
        }
        public static clsLicenseClasses GetClassByID(int ID)
        {
            string Name = "";
            string ClassDescription = "";
            byte MinAllowedAge = 0;
            byte DefaultValidityLength = 0;
            decimal ClassFees = -1;

            if (LicenseClassesAccess.GetClassByID(ID, ref Name, ref ClassDescription, ref MinAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(ID, Name, ClassDescription, DefaultValidityLength, MinAllowedAge, ClassFees);
            }
            else
            {
                return null;
            }
        }

    }
}
