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
    public class clsLikedSong
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int LikedSongID { get; set; }
        public int SongID { get; set; }
        public int PersonID { get; set; }



        public clsLikedSong()
        {
            LikedSongID = -1;
            SongID = -1;
            PersonID = -1;

            mode = enMode.eAddNew;
        }

        private clsLikedSong(int LikedSongID, int SongID, int PersonID)
        {

            this.LikedSongID = LikedSongID;
            this.SongID = SongID;
            this.PersonID = PersonID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.LikedSongID = clsLikedSongsDataAccess.AddNewRow(this.SongID, this.PersonID);

            return this.LikedSongID != -1;

        }

        bool _UpdateRow()
        {

            return clsLikedSongsDataAccess.UpdateRow(this.LikedSongID, this.SongID, this.PersonID);


        }

        public static clsLikedSong FindByLikedSongID(int LikedSongID)
        {

            int SongID = -1;
            int PersonID = -1;


            if (clsLikedSongsDataAccess.GetRowInfoByLikedSongID(LikedSongID, ref SongID, ref PersonID))
            {

                return new clsLikedSong(LikedSongID, SongID, PersonID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsLikedSongsDataAccess.GetAllRows();
            return DT;
        }

        public static DataTable GetAllRowsByPersonID(int PersonID)
        {
            DataTable DT = clsLikedSongsDataAccess.GetAllRowsByPersonID(PersonID);
            return DT;
        }


        public static int GetNumberOfRows()
        {
            return clsLikedSongsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int LikedSongID)
        {
            return (clsLikedSongsDataAccess.DeleteRow(LikedSongID));
        }

        public static bool DoesRowExist(int LikedSongID)
        {
            return (clsLikedSongsDataAccess.DoesRowExist(LikedSongID));
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
