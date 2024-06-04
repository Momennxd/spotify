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
    public class clsArtistsSong
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int ArtistsSongID { get; set; }
        public int ArtistID { get; set; }
        public int SongID { get; set; }



        public clsArtistsSong()
        {
            ArtistsSongID = -1;
            ArtistID = -1;
            SongID = -1;

            mode = enMode.eAddNew;
        }

        private clsArtistsSong(int ArtistsSongID, int ArtistID, int SongID)
        {

            this.ArtistsSongID = ArtistsSongID;
            this.ArtistID = ArtistID;
            this.SongID = SongID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.ArtistsSongID = clsArtistsSongsDataAccess.AddNewRow(this.ArtistID, this.SongID);

            return this.ArtistsSongID != -1;

        }

        bool _UpdateRow()
        {

            return clsArtistsSongsDataAccess.UpdateRow(this.ArtistsSongID, this.ArtistID, this.SongID);


        }

        public static clsArtistsSong FindByArtistsSongID(int ArtistsSongID)
        {

            int ArtistID = -1;
            int SongID = -1;


            if (clsArtistsSongsDataAccess.GetRowInfoByArtistsSongID(ArtistsSongID, ref ArtistID, ref SongID))
            {

                return new clsArtistsSong(ArtistsSongID, ArtistID, SongID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsArtistsSongsDataAccess.GetAllRows();
            return DT;
        }
        public static DataTable GetAllRowsBySongID(int SongID)
        {
            DataTable DT = clsArtistsSongsDataAccess.GetAllRowsBySongID(SongID);
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsArtistsSongsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int ArtistsSongID)
        {
            return (clsArtistsSongsDataAccess.DeleteRow(ArtistsSongID));
        }

        public static bool DoesRowExist(int ArtistsSongID)
        {
            return (clsArtistsSongsDataAccess.DoesRowExist(ArtistsSongID));
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
