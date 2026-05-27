using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissDVLD
{
    public class clsPeople
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }

        public string ThirdName { set; get; }

        public string LastName { set; get; }

        public string Email { set; get; }

        public int Gendor { set; get; }

        public string FullName {

            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }

        public string ImagePath { set; get; }

        public string NatoinlNo { set; get; }


        public int CountryID { set; get; }


        public clsCountry CountryInfo;

        public clsPeople()
        {
            ID = -1;
            Email = "";
            Address = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            ImagePath = "";
            DateOfBirth = DateTime.Now;
            CountryID = -1;
            Phone = "";
            NatoinlNo = "";
            Gendor = -1;



            Mode = enMode.AddNew;

        }

        private clsPeople(int ID, String FirstName, string SecondName ,String ThirdName, string LastName, String Email, string Phone, string Address
            , string ImagePath, int CountryID, DateTime DateOfBirth,string NationalNo,int Gendor)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.NatoinlNo = NationalNo;
            this.Gendor = Gendor;
            this.ThirdName = ThirdName;
            this.SecondName = SecondName;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this.CountryInfo = clsCountry.FindCountry(this.CountryID);

            Mode = enMode.Update;

        }



        public static clsPeople FindPerson(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Address = "", Phone = "", imagePath = "", SecondName = "", ThirdName = "" , NationlNo ="";
            int CountryID = -1, Gendor = -1;
            DateTime BirthOfDate = DateTime.Now;


            if (PeopleAccess.GetPersonByID(ID, ref FirstName,ref SecondName,ref ThirdName, ref LastName,ref NationlNo, ref Email, ref Phone, ref Address, ref CountryID,ref Gendor, ref imagePath, ref BirthOfDate))
            {
                return new clsPeople(ID, FirstName,SecondName ,ThirdName, LastName, Email, Phone, Address, imagePath, CountryID, BirthOfDate , NationlNo ,Gendor);

            }
            else
            {
                return null;
                
            }
        }

        public static clsPeople FindPerson(string NationalNo)
        {
            string FirstName = "", LastName = "", Email = "", Address = "", Phone = "", imagePath = "", SecondName = "", ThirdName = "" ;
            int CountryID = -1, Gendor = -1, ID = -1;
            DateTime BirthOfDate = DateTime.Now;


            if (PeopleAccess.GetPersonByNationlNo(NationalNo,ref  ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Email, ref Phone, ref Address, ref CountryID, ref Gendor, ref imagePath, ref BirthOfDate))
            {
                return new clsPeople(ID, FirstName, SecondName, ThirdName, LastName, Email, Phone, Address, imagePath, CountryID, BirthOfDate, NationalNo, Gendor);

            }
            else
            {
                return null;

            }
        }

        private bool _AddNew()
        {
            this.ID = PeopleAccess.AddPerson(FirstName,SecondName,ThirdName, LastName,NatoinlNo, Email, Phone, Address, CountryID,Gendor, ImagePath,  DateOfBirth);

            return (this.ID != -1);
        }
        private bool _Update()
        {
            return (PeopleAccess.UpdatePeople(ID, FirstName, SecondName, ThirdName, LastName, NatoinlNo, Email, Phone, Address, CountryID, Gendor, ImagePath,DateOfBirth));

        }

        public static bool DeletePerson(int ID)
        {
            return PeopleAccess.DeletePeople(ID);
        }

        public static DataTable GetPeoples()
        {
            return PeopleAccess.GetPeople();
        }

        public static bool IsExist(int ID)
        {
            return PeopleAccess.IsExsit(ID);
        }

        public static bool IsExist(string NationalNo)
        {
            return PeopleAccess.IsExsit(NationalNo);
        }

        public static bool IsDriver(int PersonID)
        {
            return PeopleAccess.IsDriver(PersonID);
        }




        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    if (_Update())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false;
        }
    }
}

