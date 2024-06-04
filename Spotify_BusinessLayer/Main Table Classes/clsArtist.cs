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
    public class clsArtist
    {
        public enum enMode { eAddNew = 0, eUpdate = 1 }
        public enMode mode = enMode.eAddNew;


        public int ArtistID { get; set; }
        public int UserID { get; set; }
        public string NickName { get; set; }
        public string Bio { get; set; }
        public string PagePicPath { get; set; }



        public clsArtist()
        {
            ArtistID = -1;
            UserID = -1;
            NickName = "";
            Bio = "";
            PagePicPath = "";

            mode = enMode.eAddNew;
        }

        private clsArtist(int ArtistID, int UserID, string NickName, string Bio, string PagePicPath)
        {

            this.ArtistID = ArtistID;
            this.UserID = UserID;
            this.NickName = NickName;
            this.Bio = Bio;
            this.PagePicPath = PagePicPath;

            mode = enMode.eUpdate;

        }



        bool _AddNewRow()
        {

            this.ArtistID = clsArtistsDataAccess.AddNewRow(this.UserID, this.NickName, this.Bio, this.PagePicPath);

            return this.ArtistID != -1;

        }

        bool _UpdateRow()
        {

            return clsArtistsDataAccess.UpdateRow(this.ArtistID, this.UserID, this.NickName, this.Bio, this.PagePicPath);


        }

        public static clsArtist FindByArtistID(int ArtistID)
        {

            int UserID = -1;
            string NickName = "";
            string Bio = "";
            string PagePicPath = "";


            if (clsArtistsDataAccess.GetRowInfoByArtistID(ArtistID, ref UserID, ref NickName, ref Bio, ref PagePicPath))
            {

                return new clsArtist(ArtistID, UserID, NickName, Bio, PagePicPath);
            }
            else
                return null;
        }





        public static clsArtist FindByUserID(int UserID)
        {

            int ArtistID = -1;
            string NickName = "";
            string Bio = "";
            string PagePicPath = "";


            if (clsArtistsDataAccess.GetRowInfoByUserID(ref ArtistID, UserID, ref NickName, ref Bio, ref PagePicPath))
            {

                return new clsArtist(ArtistID, UserID, NickName, Bio, PagePicPath);
            }
            else
                return null;
        }






        public static DataTable GetAllRows()
        {
            DataTable DT = clsArtistsDataAccess.GetAllRows();
            return DT;
        }

        public static int GetNumberOfRows()
        {
            return clsArtistsDataAccess.GetNumberOfRows();
        }

        public static bool DeleteRow(int ArtistID)
        {
            return (clsArtistsDataAccess.DeleteRow(ArtistID));
        }

        public static bool DoesRowExist(int ArtistID)
        {
            return (clsArtistsDataAccess.DoesRowExist(ArtistID));
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
