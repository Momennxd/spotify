using Spotify_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_PresentationLayer.Playlists
{
    public partial class frmLikesSongs : Form
    {
        public frmLikesSongs()
        {
            InitializeComponent();
        }



        private void frmLikesSongs_Load(object sender, EventArgs e)
        {
            ctrlLikedSongsPlaylist1.DisplayLikedSongs(clsScene.LoggedUser.UserID);
        }
    }
}
