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
    public class clsSong
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;

      
        public int SongID { get; set; }
        public string SongName { get; set; }
        public string ImagePath { get; set; }
        public string SongPath { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PlayCount { get; set; }
        public int GenreID { get; set; }



        public clsSong()
        {
            SongID = -1;
            SongName = "";
            ImagePath = "";
            SongPath = "";
            Duration = -1;
            ReleaseDate = DateTime.MinValue;
            PlayCount = -1;
            GenreID = -1;

            mode = enMode.eAddNew;
        }

        private clsSong(int SongID, string SongName, string ImagePath, string SongPath, int Duration, DateTime ReleaseDate, int PlayCount, int GenreID)
        {

            this.SongID = SongID;
            this.SongName = SongName;
            this.ImagePath = ImagePath;
            this.SongPath = SongPath;
            this.Duration = Duration;
            this.ReleaseDate = ReleaseDate;
            this.PlayCount = PlayCount;
            this.GenreID = GenreID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.SongID = clsSongsDataAccess.AddNewRow(this.SongName, this.ImagePath, this.SongPath, this.Duration, this.ReleaseDate, this.PlayCount, this.GenreID);

            return this.SongID != -1;

        }

        bool _UpdateRow()
        {

            return clsSongsDataAccess.UpdateRow(this.SongID, this.SongName, this.ImagePath, this.SongPath, this.Duration, this.ReleaseDate, this.PlayCount, this.GenreID);


        }

        public static clsSong FindBySongID(int SongID)
        {

            string SongName = "";
            string ImagePath = "";
            string SongPath = "";
            int Duration = -1;
            DateTime ReleaseDate = DateTime.MinValue;
            int PlayCount = -1;
            int GenreID = -1;


            if (clsSongsDataAccess.GetRowInfoBySongID(SongID, ref SongName, ref ImagePath, ref SongPath,
                ref Duration, ref ReleaseDate, ref PlayCount, ref GenreID))
            {

                return new clsSong(SongID, SongName, ImagePath, SongPath, Duration, ReleaseDate, PlayCount, GenreID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsSongsDataAccess.GetAllRows();
            return DT;
        }

        public static DataTable GetLikedSongsByPerson(int PersonID)
        {
            DataTable DT = clsSongsDataAccess.GetLikedSongsByPerson(PersonID);
            return DT;
        }


        public static int GetNumberOfRows()
        {
            return clsSongsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int SongID)
        {
            return (clsSongsDataAccess.DeleteRow(SongID));
        }

        public static bool DoesRowExist(int SongID)
        {
            return (clsSongsDataAccess.DoesRowExist(SongID));
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
