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
    public class clsSongsPlaylist
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int SongPlaylistID { get; set; }
        public int SongID { get; set; }
        public int PlaylistID { get; set; }



        public clsSongsPlaylist()
        {
            SongPlaylistID = -1;
            SongID = -1;
            PlaylistID = -1;

            mode = enMode.eAddNew;
        }

        private clsSongsPlaylist(int SongPlaylistID, int SongID, int PlaylistID)
        {

            this.SongPlaylistID = SongPlaylistID;
            this.SongID = SongID;
            this.PlaylistID = PlaylistID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.SongPlaylistID = clsSongsPlaylistsDataAccess.AddNewRow(this.SongID, this.PlaylistID);

            return this.SongPlaylistID != -1;

        }

        bool _UpdateRow()
        {

            return clsSongsPlaylistsDataAccess.UpdateRow(this.SongPlaylistID, this.SongID, this.PlaylistID);


        }

        public static clsSongsPlaylist FindBySongPlaylistID(int SongPlaylistID)
        {

            int SongID = -1;
            int PlaylistID = -1;


            if (clsSongsPlaylistsDataAccess.GetRowInfoBySongPlaylistID(SongPlaylistID, ref SongID, ref PlaylistID))
            {

                return new clsSongsPlaylist(SongPlaylistID, SongID, PlaylistID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsSongsPlaylistsDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsSongsPlaylistsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int SongPlaylistID)
        {
            return (clsSongsPlaylistsDataAccess.DeleteRow(SongPlaylistID));
        }

        public static bool DoesRowExist(int SongPlaylistID)
        {
            return (clsSongsPlaylistsDataAccess.DoesRowExist(SongPlaylistID));
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
