using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public clsCountry() { Name = "";
            ID = -1;
        }

        public clsCountry(int ID , string Name)
        {
            this.Name = Name;
            this.ID = ID;
        }
        public static clsCountry FindCountry(int ID)
        {
            string Name = "";
         


            if (CountryAccess.GetCountryByID(ID, ref Name))
            {
                return new clsCountry(ID, Name);

            }
            else
            {
                return null;
            }
        }
        public static clsCountry FindCountry(string Name)
        {
            int ID = -1;



            if (CountryAccess.GetCountryByName(ref ID,  Name))
            {
                return new clsCountry(ID, Name);

            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCountries()
        {
            return CountryAccess.GetCountries();
        }



    }
}
