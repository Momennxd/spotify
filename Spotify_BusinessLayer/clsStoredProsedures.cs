using Spotify_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_BusinessLayer
{
    public static class clsStoredProsedures
    {
        public static DataTable GetSongArtists(int songID)
        {
            if (songID > 0)
                return clsStoredProceduresDA.sp_GetSongArtists(songID);

            return null;
        }

        public static bool sp_DoesUserLikeSong(int songID, int UserID)
        {

            return clsStoredProceduresDA.sp_DoesUserLikeSong(songID, UserID);

        }


    }
}
