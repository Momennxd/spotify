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
    public class clPlaylist
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int PlaylistID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }



        public clPlaylist()
        {
            PlaylistID = -1;
            Title = "";
            Description = "";
            ImagePath = "";
            ReleaseDate = DateTime.MinValue;

            mode = enMode.eAddNew;
        }

        private clPlaylist(int PlaylistID, string Title, string Description, string ImagePath, DateTime ReleaseDate)
        {

            this.PlaylistID = PlaylistID;
            this.Title = Title;
            this.Description = Description;
            this.ImagePath = ImagePath;
            this.ReleaseDate = ReleaseDate;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.PlaylistID = clsPlaylistsDataAccess.AddNewRow(this.Title, this.Description, this.ImagePath, this.ReleaseDate);

            return this.PlaylistID != -1;

        }

        bool _UpdateRow()
        {

            return clsPlaylistsDataAccess.UpdateRow(this.PlaylistID, this.Title, this.Description, this.ImagePath, this.ReleaseDate);


        }

        public static clPlaylist FindByPlaylistID(int PlaylistID)
        {

            string Title = "";
            string Description = "";
            string ImagePath = "";
            DateTime ReleaseDate = DateTime.MinValue;


            if (clsPlaylistsDataAccess.GetRowInfoByPlaylistID(PlaylistID, ref Title, ref Description, ref ImagePath, ref ReleaseDate))
            {

                return new clPlaylist(PlaylistID, Title, Description, ImagePath, ReleaseDate);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsPlaylistsDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsPlaylistsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int PlaylistID)
        {
            return (clsPlaylistsDataAccess.DeleteRow(PlaylistID));
        }

        public static bool DoesRowExist(int PlaylistID)
        {
            return (clsPlaylistsDataAccess.DoesRowExist(PlaylistID));
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
