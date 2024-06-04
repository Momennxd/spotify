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
    public class clsPlaylistsCreator
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int PlaylistCreatorID { get; set; }
        public int CreatorID { get; set; }
        public int PlaylistID { get; set; }



        public clsPlaylistsCreator()
        {
            PlaylistCreatorID = -1;
            CreatorID = -1;
            PlaylistID = -1;

            mode = enMode.eAddNew;
        }

        private clsPlaylistsCreator(int PlaylistCreatorID, int CreatorID, int PlaylistID)
        {

            this.PlaylistCreatorID = PlaylistCreatorID;
            this.CreatorID = CreatorID;
            this.PlaylistID = PlaylistID;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.PlaylistCreatorID = clsPlaylistsCreatorDataAccess.AddNewRow(this.CreatorID, this.PlaylistID);

            return this.PlaylistCreatorID != -1;

        }

        bool _UpdateRow()
        {

            return clsPlaylistsCreatorDataAccess.UpdateRow(this.PlaylistCreatorID, this.CreatorID, this.PlaylistID);


        }

        public static clsPlaylistsCreator FindByPlaylistCreatorID(int PlaylistCreatorID)
        {

            int CreatorID = -1;
            int PlaylistID = -1;


            if (clsPlaylistsCreatorDataAccess.GetRowInfoByPlaylistCreatorID(PlaylistCreatorID, ref CreatorID, ref PlaylistID))
            {

                return new clsPlaylistsCreator(PlaylistCreatorID, CreatorID, PlaylistID);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsPlaylistsCreatorDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsPlaylistsCreatorDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int PlaylistCreatorID)
        {
            return (clsPlaylistsCreatorDataAccess.DeleteRow(PlaylistCreatorID));
        }

        public static bool DoesRowExist(int PlaylistCreatorID)
        {
            return (clsPlaylistsCreatorDataAccess.DoesRowExist(PlaylistCreatorID));
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
