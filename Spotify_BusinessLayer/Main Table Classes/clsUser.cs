using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotify_DataAccessLayer;

namespace Spotify_BusinessLayer
{
    public class clsUser
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }



        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = false;

            mode = enMode.eAddNew;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.UserID = clsUsersDataAccess.AddNewRow(this.PersonID, this.UserName, this.Password, this.IsActive);

            return this.UserID != -1;

        }

        bool _UpdateRow()
        {

            return clsUsersDataAccess.UpdateRow(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);


        }

        public static clsUser FindByUserID(int UserID)
        {

            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;


            if (clsUsersDataAccess.GetRowInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {

                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }





        public static clsUser FindByPersonID(int PersonID)
        {

            int UserID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;


            if (clsUsersDataAccess.GetRowInfoByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref IsActive))
            {

                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }





        public static clsUser FindByUserName(string UserName)
        {

            int UserID = -1;
            int PersonID = -1;
            string Password = "";
            bool IsActive = false;


            if (clsUsersDataAccess.GetRowInfoByUserName(ref UserID, ref PersonID, UserName, ref Password, ref IsActive))
            {

                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsUsersDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsUsersDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int UserID)
        {
            return (clsUsersDataAccess.DeleteRow(UserID));
        }

        public static bool DoesRowExist(int UserID)
        {
            return (clsUsersDataAccess.DoesRowExist(UserID));
        }

        public bool Save()
        {
            switch (mode)
            {
                case enMode.eAddNew:
                    {
                        if (_AddNewRow())
                        {
                            mode = enMode.eUpdate;
                            return true;
                        }
                        else
                            return false;

                    }
                case enMode.eUpdate:

                    return _UpdateRow();

            }

            return false;
        }





    }
}
