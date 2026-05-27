using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsAppType
    {

        public int ID { get; set; }
        public string Name { get; set; }

        public decimal Fees { get; set; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsAppType()
        {
            ID = -1;
            Name = "";
            Fees = -1;
            Mode = enMode.AddNew;

        }

        public clsAppType(int id, string name, decimal fees)
        {
            ID = id;
            Name = name;
            Fees = fees;
            Mode = enMode.Update;
        }


        public static DataTable GetAllTypes()
        {
            return AppTypesAccess.GetAppTypes();
        }

        public static clsAppType FindAppType(int ID)
        {
            string Name = "";
            decimal Fees = -1;



            if (AppTypesAccess.GetAppTypeByID(ID, ref Name, ref Fees))
            {
                return new clsAppType(ID, Name, Fees);

            }
            else
            {
                return null;
            }
        }
        private bool _Update()
        {
            return (AppTypesAccess.UpdateAppType(ID, Name, Fees));

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
