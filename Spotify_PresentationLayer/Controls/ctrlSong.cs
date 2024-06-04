using Spotify_BusinessLayer;
using Spotify_PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spotify_PresentationLayer.Controls
{
    public partial class ctrlSong : UserControl
    {
        public ctrlSong(clsSong Song, int SongIndexInPlaylist, int PlaylistID)
        {
            InitializeComponent();



            this._Song = Song;
            this.SongIndexInPlaylist = SongIndexInPlaylist;
            this.PlaylistID = PlaylistID;
        }

        //class members

        /// <summary>
        /// If the playlist ID is 0 that means the playlist is the liked songs by the current logged user,
        /// otherwise it is generated automaticly by a foreach loop when the songs get loaded.
        /// </summary>
        public int PlaylistID { get; set; }


        private clsSong _Song;

        public clsSong Song { get { return _Song; } }

        public int SongIndexInPlaylist { get; set; }

        private bool _IsPlayed = false;

        public bool IsPlayed { get {  return _IsPlayed; } }

        //events


        public event Action<clsArtist> ArtistNameClick;
        protected virtual void ArtistName_Click(clsArtist Artist)
        {
            Action<clsArtist> handler = ArtistNameClick;
            if (handler != null)
            {
                handler(Artist); // Raise the event with the parameter
            }
        }



        public event Action<bool> LikeClick;
        protected virtual void Like_Click(bool Like)
        {
            Action<bool> handler = LikeClick;
            if (handler != null)
            {
                handler(Like); // Raise the event with the parameter
            }
        }


        public event Action<clsSong> OptionsClick;
        protected virtual void Options_Click(clsSong Song)
        {
            Action<clsSong> handler = OptionsClick;
            if (handler != null)
            {
                handler(Song); // Raise the event with the parameter
            }
        }

        public class PlayPauseEventArgs : EventArgs
        {

            public bool IsPlay {  get; set; }

            public clsSong song { get; set; }


            public PlayPauseEventArgs(clsSong song, bool IsPlay)
            {
                this.IsPlay = IsPlay;
                this.song = song;
            }
        }


        public event Action<object, PlayPauseEventArgs> PlayPauseClick;
        protected virtual void PlayPause_Click(object sender, PlayPauseEventArgs eventArgs)
        {
            Action<object, PlayPauseEventArgs> handler = PlayPauseClick;
            if (handler != null)
            {
                handler(sender, eventArgs); // Raise the event with the parameter
            }
        }




        //private functions

       






        //public functions

        public void DisplaySongData(clsSong Song)
        {

            if (Song == null)
                return;

            //setting the song
            _Song = Song;


            lblArtist.Text = string.Join( "," ,
                clsSpotifySharedMethods.ConcatenateSongArtists(
                    clsStoredProsedures.GetSongArtists(Song.SongID)));

            pbSongPic.ImageLocation = Song.ImagePath;
            lblSongName.Text = Song.SongName;
            lblDuration.Text = clsSpotifySharedMethods.GetSongDurationStringFormat(Song.Duration);
            lblListeners.Text = Song.PlayCount.ToString();

            if (clsPlayedSong.PlayedSong.SongID == Song.SongID)
                clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause, clsPlayedSong.IsPlayed);

        }

        /// <summary>
        /// displaying the song passed by the constructor (if song is null it won't be displayed)
        /// </summary>
        public void DisplaySongData()
        {

            if (Song == null)
                return;

            //setting the song
            _Song = Song;


            lblArtist.Text = string.Join(",",
                clsSpotifySharedMethods.ConcatenateSongArtists(
                    clsStoredProsedures.GetSongArtists(Song.SongID)));

            pbSongPic.ImageLocation = Song.ImagePath;
            lblSongName.Text = Song.SongName;
            lblDuration.Text = clsSpotifySharedMethods.GetSongDurationStringFormat(Song.Duration);
            lblListeners.Text = Song.PlayCount.ToString();

            if (clsPlayedSong.PlayedSong.SongID == Song.SongID)
                clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause, clsPlayedSong.IsPlayed);

        }


        public void Play()
        {
            if (clsPlayedSong.PlayedSong.SongID != _Song.SongID)
                clsPlayedSong.SetSong(Song);


            clsPlayedSong.Play();
            _IsPlayed = true;
            clsScene.SongsQueue.SetCurrentPlayedSong(SongIndexInPlaylist);

            
        }

        public void Pause()
        {
            if (clsPlayedSong.PlayedSong != null && _Song.SongID == clsPlayedSong.PlayedSong.SongID)
            {
                clsPlayedSong.Pause();
                _IsPlayed = false;

            }
        }

        public void DisplayAsPlayed(bool Play)
        {
            clsSpotifySharedMethods.SetPlayPauseButton(btnPlayPause, Play);
        }

        public void PerformPlayPause()
        {
            clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(btnPlayPause,
                   Resources.icons8_play_100, Resources.icons8_pause_1001);

            if (btnPlayPause.Tag.ToString() == "1")
            {
                Play();
                clsScene.frmMainScreen.EnableSongTimer(true);
                PlayPauseClick?.Invoke(this, new PlayPauseEventArgs(_Song, true));
            }
            else
            {
                Pause();
                clsScene.frmMainScreen.EnableSongTimer(false);
                PlayPauseClick?.Invoke(this, new PlayPauseEventArgs(_Song, false));

            }

            //updating the song bar UI in the main screen
            clsScene.frmMainScreen.UpdateSongBarUI(_Song);
        }



        //private events functions

        private void lblAlbumName_Click(object sender, EventArgs e)
        {
            //Waiting for the main clsAlbum class.
        }

        private void btnHeart_Click(object sender, EventArgs e)
        {
            clsSpotifySharedMethods.PerfomTwoStatesButtonStyle(btnLikeSong,
                    Resources.icons8_heart_48, Resources.icons8_heart_1001);


            if (LikeClick != null)
                Like_Click(btnLikeSong.Tag.ToString() == "1");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (OptionsClick != null)
                Options_Click(_Song);
        }


        private void ctrlSong_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(44, 43, 48);
        }


        private void ctrlSong_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;

        }

        private void btnControls_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(44, 43, 48);

        }             

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.White;
            this.BackColor = Color.FromArgb(44, 43, 48);

        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.FromArgb(178, 178, 178);

        }

        private void Controls_MouseCLick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(91, 90, 98);
        }

        public void btnPlayPause_Click(object sender, EventArgs e)
        {
            PerformPlayPause();
        }
    }
}
