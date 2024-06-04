using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsPlaylistsDataAccess
    {
        public static bool GetRowInfoByPlaylistID(int PlaylistID, ref string Title, ref string Description, ref string ImagePath, ref DateTime ReleaseDate)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM Playlists WHERE PlaylistID = @PlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    Title = (string)Reader["Title"];

                    if (Reader["Description"] != DBNull.Value)
                    {
                        Description = (string)Reader["Description"];
                    }
                    ImagePath = (string)Reader["ImagePath"];
                    ReleaseDate = (DateTime)Reader["ReleaseDate"];


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


        public static int AddNewRow(string Title, string Description, string ImagePath, DateTime ReleaseDate)
        {

            int PlaylistID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO Playlists 
                               (Title, Description, ImagePath, ReleaseDate)     
                             VALUES
                               (@Title, @Description, @ImagePath, @ReleaseDate)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@Title", Title);

            if (Description != null && Description.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@Description", Description);
            else
                Command.Parameters.AddWithValue("@Description", DBNull.Value);
            Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    PlaylistID = InsertedID;
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


            return PlaylistID;

        }
        public static bool UpdateRow(int PlaylistID, string Title, string Description, string ImagePath, DateTime ReleaseDate)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE Playlists SET 
		Title = @Title,
		Description = @Description,
		ImagePath = @ImagePath,
		ReleaseDate = @ReleaseDate
		 WHERE PlaylistID = @PlaylistID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);
            Command.Parameters.AddWithValue("@Title", Title);

            if (Description != null && Description.ToString() != string.Empty)
                Command.Parameters.AddWithValue("@Description", Description);
            else
                Command.Parameters.AddWithValue("@Description", DBNull.Value);
            Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);


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

            string Query = @"SELECT * FROM Playlists";

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

            string Query = "SELECT Count(*) FROM Playlists WHERE PlaylistID is not null";

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

        public static bool DeleteRow(int PlaylistID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE Playlists WHERE PlaylistID = @PlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);


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



        public static bool DoesRowExist(int PlaylistID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM Playlists WHERE PlaylistID = @PlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);


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
