using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsArtistsSongsDataAccess
    {
        public static bool GetRowInfoByArtistsSongID(int ArtistsSongID, ref int ArtistID, ref int SongID)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM ArtistsSongs WHERE ArtistsSongID = @ArtistsSongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistsSongID", ArtistsSongID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    ArtistID = (int)Reader["ArtistID"];
                    SongID = (int)Reader["SongID"];


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


        public static int AddNewRow(int ArtistID, int SongID)
        {

            int ArtistsSongID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO ArtistsSongs 
                               (ArtistID, SongID)     
                             VALUES
                               (@ArtistID, @SongID)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistID", ArtistID);
            Command.Parameters.AddWithValue("@SongID", SongID);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ArtistsSongID = InsertedID;
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


            return ArtistsSongID;

        }
        public static bool UpdateRow(int ArtistsSongID, int ArtistID, int SongID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE ArtistsSongs SET 
		ArtistID = @ArtistID,
		SongID = @SongID
		 WHERE ArtistsSongID = @ArtistsSongID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@ArtistsSongID", ArtistsSongID);
            Command.Parameters.AddWithValue("@ArtistID", ArtistID);
            Command.Parameters.AddWithValue("@SongID", SongID);


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

            string Query = @"SELECT * FROM ArtistsSongs";

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

        public static DataTable GetAllRowsBySongID(int SongID)
        {

            DataTable DT = new DataTable();

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"SELECT * FROM ArtistsSongs WHERE SongID = @SongID";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@SongID", SongID);

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

            string Query = "SELECT Count(*) FROM ArtistsSongs WHERE ArtistsSongID is not null";

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

        public static bool DeleteRow(int ArtistsSongID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE ArtistsSongs WHERE ArtistsSongID = @ArtistsSongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistsSongID", ArtistsSongID);


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



        public static bool DoesRowExist(int ArtistsSongID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM ArtistsSongs WHERE ArtistsSongID = @ArtistsSongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ArtistsSongID", ArtistsSongID);


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
