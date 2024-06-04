using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsPeopleDataAccess
    {
        public static bool GetRowInfoByPersonID(int PersonID, ref string FirstName, ref string SecondName, ref string LastName, ref string Email, ref bool Gender, ref DateTime DateofBirth, ref string ProfilePicPath, ref int CountryID)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)Reader["FirstName"];

                    if (Reader["SecondName"] != DBNull.Value)
                    {
                        SecondName = (string)Reader["SecondName"];
                    }
                    LastName = (string)Reader["LastName"];
                    Email = (string)Reader["Email"];
                    Gender = (bool)Reader["Gender"];
                    DateofBirth = (DateTime)Reader["DateofBirth"];

                    if (Reader["ProfilePicPath"] != DBNull.Value)
                    {
                        ProfilePicPath = (string)Reader["ProfilePicPath"];
                    }
                    CountryID = (int)Reader["CountryID"];


                }

                Reader.Close();

            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return IsFound;


        }


        public static int AddNewRow(string FirstName, string SecondName, string LastName, string Email, bool Gender, DateTime DateofBirth, string ProfilePicPath, int CountryID)
        {

            int PersonID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO People 
                               (FirstName, SecondName, LastName, Email, Gender, DateofBirth, ProfilePicPath, CountryID)     
                             VALUES
                               (@FirstName, @SecondName, @LastName, @Email, @Gender, @DateofBirth, @ProfilePicPath, @CountryID)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@FirstName", FirstName);

            if (SecondName != null && SecondName.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@SecondName", SecondName);
            else
                Command.Parameters.AddWithValue("@SecondName", DBNull.Value);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@DateofBirth", DateofBirth);

            if (ProfilePicPath != null && ProfilePicPath.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@ProfilePicPath", ProfilePicPath);
            else
                Command.Parameters.AddWithValue("@ProfilePicPath", DBNull.Value);
            Command.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }

            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }


            return PersonID;

        }
        public static bool UpdateRow(int PersonID, string FirstName, string SecondName, string LastName, string Email, bool Gender, DateTime DateofBirth, string ProfilePicPath, int CountryID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE People SET 
		FirstName = @FirstName,
		SecondName = @SecondName,
		LastName = @LastName,
		Email = @Email,
		Gender = @Gender,
		DateofBirth = @DateofBirth,
		ProfilePicPath = @ProfilePicPath,
		CountryID = @CountryID
		 WHERE PersonID = @PersonID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@FirstName", FirstName);

            if (SecondName != null && SecondName.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@SecondName", SecondName);
            else
                Command.Parameters.AddWithValue("@SecondName", DBNull.Value);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Gender", Gender);
            Command.Parameters.AddWithValue("@DateofBirth", DateofBirth);

            if (ProfilePicPath != null && ProfilePicPath.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@ProfilePicPath", ProfilePicPath);
            else
                Command.Parameters.AddWithValue("@ProfilePicPath", DBNull.Value);
            Command.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {
                connection.Open();
                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);

        }

        public static DataTable GetAllRows()
        {

            DataTable DT = new DataTable();

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"SELECT * FROM People";

            SqlCommand Command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                while (Reader.HasRows)
                {
                    DT.Load(Reader);
                }

                Reader.Close();


            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return DT;

        }

        public static int GetNumberOfRows()
        {
            int NumberOfRows = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Count(*) FROM People WHERE PersonID is not null";

            SqlCommand Command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int AcualNum))
                {
                    NumberOfRows = AcualNum;
                }


            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return NumberOfRows;
        }

        public static bool DeleteRow(int PersonID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE People WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return (RowsAffected > 0);

        }



        public static bool DoesRowExist(int PersonID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null)
                {
                    IsFound = true;
                }

            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

    }
}
