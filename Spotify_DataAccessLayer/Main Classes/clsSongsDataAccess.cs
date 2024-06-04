using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_DataAccessLayer
{
    public class clsSongsDataAccess
    {
        public static bool GetRowInfoBySongID(int SongID, ref string SongName, ref string ImagePath, ref string SongPath, ref int Duration, ref DateTime ReleaseDate, ref int PlayCount, ref int GenreID)
        {

            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT * FROM Songs WHERE SongID = @SongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongID", SongID);

            try
            {
                connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    SongName = (string)Reader["SongName"];
                    ImagePath = (string)Reader["ImagePath"];
                    SongPath = (string)Reader["SongPath"];
                    Duration = (int)Reader["Duration"];
                    ReleaseDate = (DateTime)Reader["ReleaseDate"];
                    PlayCount = (int)Reader["PlayCount"];
                    GenreID = (int)Reader["GenreID"];


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


        public static int AddNewRow(string SongName, string ImagePath, string SongPath, int Duration, DateTime ReleaseDate, int PlayCount, int GenreID)
        {

            int SongID = -1;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"INSERT INTO Songs 
                               (SongName, ImagePath, SongPath, Duration, ReleaseDate, PlayCount, GenreID)     
                             VALUES
                               (@SongName, @ImagePath, @SongPath, @Duration, @ReleaseDate, @PlayCount, @GenreID)
                              SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongName", SongName);
            Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            Command.Parameters.AddWithValue("@SongPath", SongPath);
            Command.Parameters.AddWithValue("@Duration", Duration);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@PlayCount", PlayCount);
            Command.Parameters.AddWithValue("@GenreID", GenreID);


            try
            {
                connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    SongID = InsertedID;
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


            return SongID;

        }
        public static bool UpdateRow(int SongID, string SongName, string ImagePath, string SongPath, int Duration, DateTime ReleaseDate, int PlayCount, int GenreID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = $@"UPDATE Songs SET 
		SongName = @SongName,
		ImagePath = @ImagePath,
		SongPath = @SongPath,
		Duration = @Duration,
		ReleaseDate = @ReleaseDate,
		PlayCount = @PlayCount,
		GenreID = @GenreID
		 WHERE SongID = @SongID
";


            SqlCommand Command = new SqlCommand(Query, connection);


            Command.Parameters.AddWithValue("@SongID", SongID);
            Command.Parameters.AddWithValue("@SongName", SongName);
            Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            Command.Parameters.AddWithValue("@SongPath", SongPath);
            Command.Parameters.AddWithValue("@Duration", Duration);
            Command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            Command.Parameters.AddWithValue("@PlayCount", PlayCount);
            Command.Parameters.AddWithValue("@GenreID", GenreID);


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

            string Query = @"SELECT * FROM Songs";

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

        public static DataTable GetLikedSongsByPerson(int PersonID)
        {

            DataTable DT = new DataTable();

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = @"
                         SELECT        Songs.*
                         FROM            Songs INNER JOIN
                         LikedSongs ON Songs.SongID = LikedSongs.SongID where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

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

            string Query = "SELECT Count(*) FROM Songs WHERE SongID is not null";

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

        public static bool DeleteRow(int SongID)
        {

            int RowsAffected = 0;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "DELETE Songs WHERE SongID = @SongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongID", SongID);


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



        public static bool DoesRowExist(int SongID)
        {
            bool IsFound = false;

            string connectionString = "Server=.;Database=SpotifyCloneDB;User Id=sa;Password=sa123456;";
            SqlConnection connection = new SqlConnection(connectionString);

            string Query = "SELECT Found=1 FROM Songs WHERE SongID = @SongID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@SongID", SongID);


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
