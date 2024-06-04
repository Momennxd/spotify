using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsSongsPlaylistsDataAccess
    {
        public static bool GetRowInfoBySongPlaylistID(int SongPlaylistID, ref int SongID, ref int PlaylistID)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM SongsPlaylists WHERE SongPlaylistID = @SongPlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongPlaylistID", SongPlaylistID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    SongID = (int)Reader["SongID"];
                    PlaylistID = (int)Reader["PlaylistID"];


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


        public static int AddNewRow(int SongID, int PlaylistID)
        {

            int SongPlaylistID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO SongsPlaylists 
                               (SongID, PlaylistID)     
                             VALUES
                               (@SongID, @PlaylistID)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongID", SongID);
            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    SongPlaylistID = InsertedID;
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


            return SongPlaylistID;

        }
        public static bool UpdateRow(int SongPlaylistID, int SongID, int PlaylistID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE SongsPlaylists SET 
		SongID = @SongID,
		PlaylistID = @PlaylistID
		 WHERE SongPlaylistID = @SongPlaylistID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@SongPlaylistID", SongPlaylistID);
            Command.Parameters.AddWithValue("@SongID", SongID);
            Command.Parameters.AddWithValue("@PlaylistID", PlaylistID);


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

            string Query = @"SELECT * FROM SongsPlaylists";

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

            string Query = "SELECT Count(*) FROM SongsPlaylists WHERE SongPlaylistID is not null";

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

        public static bool DeleteRow(int SongPlaylistID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE SongsPlaylists WHERE SongPlaylistID = @SongPlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongPlaylistID", SongPlaylistID);


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



        public static bool DoesRowExist(int SongPlaylistID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM SongsPlaylists WHERE SongPlaylistID = @SongPlaylistID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongPlaylistID", SongPlaylistID);


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
