using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsArtistsDataAccess
    {
        public static bool GetRowInfoByArtistID(int ArtistID, ref int UserID, ref string NickName, ref string Bio, ref string PagePicPath)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM Artists WHERE ArtistID = @ArtistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistID", ArtistID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    UserID = (int)Reader["UserID"];
                    NickName = (string)Reader["NickName"];

                    if (Reader["Bio"] != DBNull.Value)
                    {
                        Bio = (string)Reader["Bio"];
                    }

                    if (Reader["PagePicPath"] != DBNull.Value)
                    {
                        PagePicPath = (string)Reader["PagePicPath"];
                    }


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


        public static bool GetRowInfoByUserID(ref int ArtistID, int UserID, ref string NickName, ref string Bio, ref string PagePicPath)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM Artists WHERE UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    ArtistID = (int)Reader["ArtistID"];
                    NickName = (string)Reader["NickName"];

                    if (Reader["Bio"] != DBNull.Value)
                    {
                        Bio = (string)Reader["Bio"];
                    }

                    if (Reader["PagePicPath"] != DBNull.Value)
                    {
                        PagePicPath = (string)Reader["PagePicPath"];
                    }


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


        public static int AddNewRow(int UserID, string NickName, string Bio, string PagePicPath)
        {

            int ArtistID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO Artists 
                               (UserID, NickName, Bio, PagePicPath)     
                             VALUES
                               (@UserID, @NickName, @Bio, @PagePicPath)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@NickName", NickName);

            if (Bio != null && Bio.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@Bio", Bio);
            else
                Command.Parameters.AddWithValue("@Bio", DBNull.Value);

            if (PagePicPath != null && PagePicPath.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@PagePicPath", PagePicPath);
            else
                Command.Parameters.AddWithValue("@PagePicPath", DBNull.Value);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ArtistID = InsertedID;
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


            return ArtistID;

        }
        public static bool UpdateRow(int ArtistID, int UserID, string NickName, string Bio, string PagePicPath)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE Artists SET 
		UserID = @UserID,
		NickName = @NickName,
		Bio = @Bio,
		PagePicPath = @PagePicPath
		 WHERE ArtistID = @ArtistID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@ArtistID", ArtistID);
            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@NickName", NickName);

            if (Bio != null && Bio.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@Bio", Bio);
            else
                Command.Parameters.AddWithValue("@Bio", DBNull.Value);

            if (PagePicPath != null && PagePicPath.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@PagePicPath", PagePicPath);
            else
                Command.Parameters.AddWithValue("@PagePicPath", DBNull.Value);


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

            string Query = @"SELECT * FROM Artists";

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

            string Query = "SELECT Count(*) FROM Artists WHERE ArtistID is not null";

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

        public static bool DeleteRow(int ArtistID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE Artists WHERE ArtistID = @ArtistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistID", ArtistID);


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



        public static bool DoesRowExist(int ArtistID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM Artists WHERE ArtistID = @ArtistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistID", ArtistID);


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
