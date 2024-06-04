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
    public class clsPerson
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public string ProfilePicPath { get; set; }
        public int CountryID { get; set; }



        public clsPerson()
        {
            PersonID = -1;
            FirstName = "";
            SecondName = "";
            LastName = "";
            Email = "";
            Gender = false;
            DateofBirth = DateTime.MinValue;
            ProfilePicPath = "";
            CountryID = -1;

            mode = enMode.eAddNew;
        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string LastName, string Email, bool Gender, DateTime DateofBirth, string ProfilePicPath, int CountryID)
        {

            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.LastName = LastName;
            this.Email = Email;
            this.Gender = Gender;
            this.DateofBirth = DateofBirth;
            this.ProfilePicPath = ProfilePicPath;
            this.CountryID = CountryID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.PersonID = clsPeopleDataAccess.AddNewRow(this.FirstName, this.SecondName, this.LastName, this.Email, this.Gender, this.DateofBirth, this.ProfilePicPath, this.CountryID);

            return this.PersonID != -1;

        }

        bool _UpdateRow()
        {

            return clsPeopleDataAccess.UpdateRow(this.PersonID, this.FirstName, this.SecondName, this.LastName, this.Email, this.Gender, this.DateofBirth, this.ProfilePicPath, this.CountryID);


        }

        public static clsPerson FindByPersonID(int PersonID)
        {

            string FirstName = "";
            string SecondName = "";
            string LastName = "";
            string Email = "";
            bool Gender = false;
            DateTime DateofBirth = DateTime.MinValue;
            string ProfilePicPath = "";
            int CountryID = -1;


            if (clsPeopleDataAccess.GetRowInfoByPersonID(PersonID, ref FirstName, ref SecondName, ref LastName, ref Email, ref Gender, ref DateofBirth, ref ProfilePicPath, ref CountryID))
            {

                return new clsPerson(PersonID, FirstName, SecondName, LastName, Email, Gender, DateofBirth, ProfilePicPath, CountryID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsPeopleDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsPeopleDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int PersonID)
        {
            return (clsPeopleDataAccess.DeleteRow(PersonID));
        }

        public static bool DoesRowExist(int PersonID)
        {
            return (clsPeopleDataAccess.DoesRowExist(PersonID));
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
