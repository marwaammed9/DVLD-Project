using DataAccessDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static BusnissDVLD.clsPeople;

namespace BusnissDVLD
{
    public class clsUser
    {
        public int UserID { get; set; }


        clsPeople Person;

        public int PersonID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public bool isActive { get; set; }

        enum eModes { eNew = 1, eUpdate = 2 };
        eModes Mode;


        public clsUser()
        {
            Mode = eModes.eNew;
            UserID = -1;
            UserName = "";
            Password = "";
            isActive = true;
            PersonID = -1;


        }

        public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            Mode = eModes.eUpdate;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = IsActive;
            this.PersonID = PersonID;
            this.Person = clsPeople.FindPerson(PersonID);

        }
        public static DataTable GetAllUsers()
        {
            return UserAccess.GetUsers();
        }

        public static clsUser FindUser(int ID)
        {
            string UserName = "", Password = "";
            int PresonID = -1;
            bool isActive = false;



            if (UserAccess.GetUserByID(ID, ref UserName, ref Password, ref PresonID, ref isActive))
            {
                return new clsUser(ID, PresonID, UserName, Password, isActive);

            }
            else
            {
                return null;

            }
        }

        public static clsUser FindUser(string userName)
        {
            int ID = -1;
            string Password = "";
            int PresonID = -1;
            bool isActive = false;



            if (UserAccess.GetUserByUserName(userName, ref ID, ref Password, ref PresonID, ref isActive))
            {
                return new clsUser(ID, PresonID, userName, Password, isActive);

            }
            else
            {
                return null;

            }
        }
        public static clsUser FindUser(string userName, string Password)
        {
            int ID = -1;
            int PresonID = -1;
            bool isActive = false;



            if (UserAccess.GetUserInfoByUsernameAndPassword(userName, Password, ref ID, ref PresonID, ref isActive))
            {
                return new clsUser(ID, PresonID, userName, Password, isActive);

            }
            else
            {
                return null;

            }
        }
        private bool _AddNew()
        {
            this.UserID = UserAccess.AddUser(UserName, Password, PersonID, isActive);

            return (this.UserID != -1);
        }
        private bool _Update()
        {
            return (UserAccess.UpdateUser(UserID, PersonID, UserName, Password, isActive));

        }
        public static bool DeleteUser(int UserID)
        {
            return UserAccess.DeleteUser(UserID);
        }

        public static bool IsExist(int ID)
        {
            return UserAccess.IsUserExist(ID);
        }
        public static bool IsExist(string UserNAme)
        {
            return UserAccess.IsUserExist(UserNAme);
        }
        public static bool isUserExistForPersonID(int PersonID)
        {
            return UserAccess.IsPersonHasUser(PersonID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case eModes.eNew:
                    if (_AddNew())
                    {
                        Mode = eModes.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case eModes.eUpdate:
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
