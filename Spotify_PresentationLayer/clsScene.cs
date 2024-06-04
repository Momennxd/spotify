using Spotify_BusinessLayer;
using Spotify_PresentationLayer.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_PresentationLayer
{
    public static class clsScene
    {

        public static clsSongsQueue SongsQueue = new clsSongsQueue();

        public static frmMainScreen frmMainScreen = null;

        public static clsUser LoggedUser = clsUser.FindByPersonID(1);

        public static List<ctrlSong> CurrentPlaylist = new List<ctrlSong>();
   
    }
}
